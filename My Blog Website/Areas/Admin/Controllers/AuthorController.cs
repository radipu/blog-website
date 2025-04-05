using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Areas.Admin.Models;
using My_Blog_Website.Data;
using System.Security.Cryptography;
using System.Text;

namespace My_Blog_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("admin/author")]
        public IActionResult Index()
        {
            List<Authors> authors = _context.authors.ToList();
            return View(authors);
        }

        [HttpGet]
        [Route("admin/author/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/author/create")]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Username,Password,Email,AboutAuth")] Authors author, IFormFile authorImage)
        {
            // Manually clear model state for image fields
            ModelState.Remove("AuthorImage");
            ModelState.Remove("ImageContentType");

            try
            {
                // Image validation
                if (authorImage == null || authorImage.Length == 0)
                {
                    ModelState.AddModelError("AuthorImage", "Author image is required");
                    return View(author);
                }

                // Validate file type/size
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedTypes.Contains(authorImage.ContentType.ToLower()))
                {
                    ModelState.AddModelError("AuthorImage", "Only JPG, PNG, or GIF allowed");
                    return View(author);
                }

                if (authorImage.Length > 2 * 1024 * 1024) // 2MB limit
                {
                    ModelState.AddModelError("AuthorImage", "File size exceeds 2MB limit");
                    return View(author);
                }

                var existingUsername = _context.authors.FirstOrDefault(a => a.Username == author.Username);
                var existingEmail = _context.authors.FirstOrDefault(a => a.Email == author.Email);
                if (existingUsername != null)
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(author);
                }
                else if (existingEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(author);
                }

                // Process image file
                using var ms = new MemoryStream();
                await authorImage.CopyToAsync(ms);
                author.AuthorImage = ms.ToArray();
                author.ImageContentType = authorImage.ContentType;

                // Final validation
                if (ModelState.IsValid)
                {
                    // Hash the plain text password before saving
                    author.Password = BCrypt.Net.BCrypt.HashPassword(author.Password);

                    _context.Add(author);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error saving author: {ex.Message}");
            }

            return View(author);
        }

        //private string HashPassword(string password)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        var passwordBytes = Encoding.UTF8.GetBytes(password);
        //        var hashBytes = sha256.ComputeHash(passwordBytes);
        //        return Convert.ToBase64String(hashBytes);
        //    }
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("admin/author/getauthorimage/{id}")]
        public IActionResult GetAuthorImage(int id)
        {
            var author = _context.authors.Find(id);
            if (author?.AuthorImage == null) return NotFound();
            return File(author.AuthorImage, author.ImageContentType);
        }

        [HttpGet]
        [Route("admin/author/edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var author = _context.authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [Route("admin/author/edit/{id}")]
        public async Task<IActionResult> Edit(Authors author, IFormFile? authorImage)
        {
            // Remove image-related model state errors since these fields are not bound on edit.
            ModelState.Remove("AuthorImage");
            ModelState.Remove("ImageContentType");

            // Retrieve the existing record to preserve image and password if needed.
            var existingAuthor = _context.authors.AsNoTracking().FirstOrDefault(a => a.AuthorId == author.AuthorId);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            // Handle password: hash the new password if provided; otherwise, keep the old hashed password.
            if (!string.IsNullOrWhiteSpace(author.Password))
            {
                author.Password = BCrypt.Net.BCrypt.HashPassword(author.Password);
            }
            else
            {
                author.Password = existingAuthor.Password;
            }

            // Process image update if a new file is provided.
            if (authorImage != null && authorImage.Length > 0)
            {
                // Validate file type.
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedTypes.Contains(authorImage.ContentType.ToLower()))
                {
                    ModelState.AddModelError("AuthorImage", "Only JPG, PNG, or GIF allowed");
                    return View(author);
                }

                // Validate file size (limit to 2MB).
                if (authorImage.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("AuthorImage", "File size exceeds 2MB limit");
                    return View(author);
                }

                using var ms = new MemoryStream();
                await authorImage.CopyToAsync(ms);
                author.AuthorImage = ms.ToArray();
                author.ImageContentType = authorImage.ContentType;
            }
            else
            {
                // If no new image is provided, keep the existing image data.
                author.AuthorImage = existingAuthor.AuthorImage;
                author.ImageContentType = existingAuthor.ImageContentType;
            }

            if (!ModelState.IsValid)
            {
                return View(author);
            }

            try
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating author: {ex.Message}");
            }

            return View(author);
        }

        [HttpGet]
        [Route("admin/author/delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var author = _context.authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [Route("admin/author/delete/{id}")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var author = _context.authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            _context.authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
