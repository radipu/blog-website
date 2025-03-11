using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Areas.Admin.Models;
using My_Blog_Website.Data;
using My_Blog_Website.Helpers;

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
            List<Posts> posts = _db.posts
                .OrderByDescending(p => p.PublishedDate)
                .ToList();
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
        public async Task<IActionResult> Create(Posts posts)
        {
            // Get the uploaded file from the request (if any)
            var featureImage = Request.Form.Files["featureImage"];

            // Remove validation for FeatureImageUrl to avoid conflicts
            ModelState.Remove("FeatureImageUrl");

            // Check if either file or URL is provided
            bool hasFile = featureImage != null && featureImage.Length > 0;
            bool hasUrl = !string.IsNullOrEmpty(posts.FeatureImageUrl);

            // Add custom validation
            if (!hasFile && !hasUrl)
            {
                ModelState.AddModelError("FeatureImageUrl", "You must upload an image or provide a URL.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle file upload
                    if (hasFile)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                        Directory.CreateDirectory(uploadsFolder);

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(featureImage.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await featureImage.CopyToAsync(stream);
                        }
                        posts.FeatureImageUrl = $"/uploads/{fileName}";
                    }
                    // Validate URL format if used
                    else if (hasUrl && !Uri.IsWellFormedUriString(posts.FeatureImageUrl, UriKind.Absolute))
                    {
                        ModelState.AddModelError("FeatureImageUrl", "Invalid URL format.");
                        return View(posts);
                    }

                    // Save to database
                    posts.Slug = URLHelper.GeneratePostSlug(posts.Title);
                    posts.PublishedDate = DateTime.Now;
                    _db.Add(posts);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }
            return View(posts);
        }

        [HttpGet]
        [Route("admin/post/published")]
        public IActionResult Published()
        {
            List<Posts> posts = _db.posts
                .Where(p => p.PostStatus == "Published")
                .OrderByDescending(p => p.PublishedDate)
                .ToList();
            return View(posts);
        }

        [HttpGet]
        [Route("admin/post/draft")]
        public IActionResult Draft()
        {
            List<Posts> posts = _db.posts
                .Where(p => p.PostStatus == "Draft")
                .OrderByDescending(p => p.PublishedDate)
                .ToList();
            return View(posts);
        }

        public IActionResult Single(int id)
        {
            var post = _db.posts.FirstOrDefault(p => p.PostId == id);
            return View(post);
        }
    }
}