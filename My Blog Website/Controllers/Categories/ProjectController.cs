using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Data;

namespace My_Blog_Website.Controllers.Categories
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var projectPosts = _context.posts
                                .Where(p => p.Categories == "Project" && p.PostStatus == "Published")
                                .OrderByDescending(p => p.LastModifiedDate ?? p.PublishedDate)
                                .ToList();

            ViewBag.PageSize = 9;
            ViewBag.TotalBookReviews = projectPosts.Count;
            return View(projectPosts);
        }
    }
}