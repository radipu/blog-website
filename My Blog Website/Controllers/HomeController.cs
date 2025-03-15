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
            // Retrieve all published posts ordered by publish date
            var allPosts = _db.posts
                              .Where(p => p.PostStatus == "Published")
                              .OrderByDescending(p => p.PublishedDate)
                              .ToList();

            // Extract and process tags
            var allTags = allPosts
                            .SelectMany(p => p.Tags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            .Select(t => t.Trim().ToLower())
                            .Distinct()
                            .OrderBy(t => t)
                            .ToList();

            // Get all How To posts (send the complete list, not just a slice)
            var howtoPosts = GetPostsByCategory(allPosts, "HowTo");

            // Build the view model with all necessary data
            var viewModel = new HomeViewModel
            {
                LatestPost = allPosts.Take(5).ToList(),
                Thoughts = GetPostsByCategory(allPosts, "Thoughts"),
                Technology = GetPostsByCategory(allPosts, "Technology"),
                Ideas = GetPostsByCategory(allPosts, "Ideas"),
                HowTo = howtoPosts, // all items are sent to the view
                Tour = GetPostsByCategory(allPosts, "Tour"),
                Developer = GetPostsByCategory(allPosts, "Developer"),
                BookReview = GetPostsByCategory(allPosts, "Book-Review"),
                Tags = allTags
            };

            // Pass extra information for client-side paging
            ViewBag.PageSize = 4;
            ViewBag.TotalHowtoPosts = howtoPosts.Count;

            return View(viewModel);
        }

        private List<Posts> GetPostsByCategory(List<Posts> posts, string category)
        {
            return posts
                .Where(p => SplitCategories(p.Categories).Contains(category))
                .OrderByDescending(p => p.PublishedDate)
                .ToList();
        }

        [HttpGet]
        [Route("/PostsByTag/{tag}")]
        public IActionResult PostsByTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return RedirectToAction("Index");

            var normalizedTag = tag.Trim().ToLower();

            var allPosts = _db.posts
                .Where(p => p.PostStatus == "Published")
                .ToList();

            var filteredPosts = allPosts
                .Where(p => p.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Any(t => t.Trim().ToLower() == normalizedTag))
                .OrderByDescending(p => p.PublishedDate)
                .ToList();

            ViewBag.Tag = tag; // For displaying in the view
            ViewBag.PageSize = 9;
            ViewBag.TotalFilteredPosts = filteredPosts.Count;
            return View(filteredPosts);
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