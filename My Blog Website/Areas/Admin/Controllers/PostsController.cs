using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Areas.Admin.Models;
using My_Blog_Website.Data;

namespace My_Blog_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PostsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("admin/posts")]
        public IActionResult Index()
        {
            List<Posts> posts = _db.posts.ToList();
            return View(posts);
        }

        [HttpGet]
        [Route("admin/post/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/post/create")]
        public async Task<IActionResult> Create(Posts posts, IFormFile featureImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Handle file upload
                    if (featureImage != null && featureImage.Length > 0)
                    {
                        // Ensure the uploads directory exists
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Generate safe filename
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(featureImage.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await featureImage.CopyToAsync(stream);
                        }

                        posts.FeatureImageUrl = $"/uploads/{fileName}";
                    }

                    posts.PublishedDate = DateTime.Now;
                    _db.Add(posts);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                    // Log the full error details
                    Console.WriteLine($"Full error: {ex}");
                }
            }
            return View(posts);
        }
    }
}