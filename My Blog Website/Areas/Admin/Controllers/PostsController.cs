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
            return View();
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
                if (featureImage != null && featureImage.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await featureImage.CopyToAsync(memoryStream);
                        posts.FeatureImage = memoryStream.ToArray();
                    }
                }

                //posts.Author = User.Identity.Name; // Assuming you're using authentication
                posts.Date = DateTime.Now;

                _db.Add(posts);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(posts);
        }

    }
}
