using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Areas.Admin.Models;
using My_Blog_Website.Data;

namespace My_Blog_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
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

                if (authorImage.Length > 2 * 1024 * 1024) // 2MB
                {
                    ModelState.AddModelError("AuthorImage", "File size exceeds 2MB limit");
                    return View(author);
                }

                var existingAuthor = _context.authors.FirstOrDefault(a => a.Username == author.Username || a.Email == author.Email);

                if (existingAuthor != null)
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(author);
                }

                // Process image
                using var ms = new MemoryStream();
                await authorImage.CopyToAsync(ms);
                author.AuthorImage = ms.ToArray();
                author.ImageContentType = authorImage.ContentType;

                // Final validation
                if (ModelState.IsValid)
                {
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

        [HttpGet]
        public IActionResult GetImage(int id)
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
        public async Task<IActionResult> Edit(Authors author)
        {
            if (ModelState.IsValid)
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
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
