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
    string submitType) // "Published" or "Draft"
        {
            // Assign PostStatus FIRST
            posts.PostStatus = submitType; // 🚨 Move this line HERE

            if (ModelState.IsValid)
            {
                try
                {
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
            else
            {
                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(posts);
        }
    }
}