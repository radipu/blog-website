using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Areas.Admin.Models;
using My_Blog_Website.Data;
using My_Blog_Website.Models;
using My_Blog_Website.ViewModels;
using System.Diagnostics;

namespace My_Blog_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            // Get all posts ordered by publish date
            var allPosts = _db.posts
                             .OrderByDescending(p => p.PublishedDate)
                             .ToList();

            var viewModel = new HomeViewModel
            {
                LatestPost = allPosts.Take(5).ToList(),

                Thoughts = GetPostsByCategory(allPosts, "Thoughts"),
                Technology = GetPostsByCategory(allPosts, "Technology"),
                Ideas = GetPostsByCategory(allPosts, "Ideas"),
                HowTo = GetPostsByCategory(allPosts, "How To")
            };

            return View(viewModel);
        }

        private List<Posts> GetPostsByCategory(List<Posts> posts, string category)
        {
            return posts
                .Where(p => SplitCategories(p.Categories).Contains(category))
                .Take(4)
                .ToList();
        }

        private List<string> SplitCategories(string categories)
        {
            return string.IsNullOrEmpty(categories)
                ? new List<string>()
                : categories.Split(',').Select(c => c.Trim()).ToList();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}