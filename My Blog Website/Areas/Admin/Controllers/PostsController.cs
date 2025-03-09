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
    string submitType) // "Published" or "Draft" from button value
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Set PostStatus from the button's value
                    posts.PostStatus = submitType;

                    // Set PublishedDate to current time
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
            return View(posts);
        }
    }
}