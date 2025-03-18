using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Data;

namespace My_Blog_Website.Areas.Public.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SearchController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Search/Search")]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Json(new List<object>());
            }

            var posts = await _db.posts
                .Where(p => p.PostStatus == "Published" &&
                            (p.Title.Contains(query) ||
                             p.PostContent.Contains(query) ||
                             p.PostDescription.Contains(query) ||
                             p.Categories.Contains(query) ||
                             p.Tags.Contains(query)))
                .OrderByDescending(p => p.PublishedDate)
                .Select(p => new { p.Title, p.Slug, p.Categories })
                .ToListAsync();

            return Json(posts);
        }
    }
}
