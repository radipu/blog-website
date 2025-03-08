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
        public async Task<IActionResult> Create(
    Posts posts,
    IFormFile featureImage,
    string finalImageData,
    string submitType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Handle status
                    posts.PostStatus = submitType;

                    // Handle image upload
                    if (featureImage != null && featureImage.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await featureImage.CopyToAsync(memoryStream);
                            posts.FeatureImage = memoryStream.ToArray();
                        }
                    }
                    else if (!string.IsNullOrEmpty(finalImageData))
                    {
                        if (finalImageData.StartsWith("data:image"))
                        {
                            var base64Data = finalImageData.Substring(finalImageData.IndexOf(",") + 1);
                            posts.FeatureImage = Convert.FromBase64String(base64Data);
                        }
                        else
                        {
                            posts.FeatureImageUrl = finalImageData;
                        }
                    }

                    // Set published date
                    posts.PublishedDate = DateTime.Now;

                    // Add to database
                    _db.Add(posts);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error saving post: {ex.Message}");
                }
            }
            else
            {
                // Log model state errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(posts);
        }


    }
}
