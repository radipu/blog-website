using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Data;

namespace My_Blog_Website.Controllers.Categories
{
    public class TourController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TourController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var tourPosts = _context.posts
                .Where(p => p.Categories == "Tour" && p.PostStatus == "Published")
                .OrderByDescending(p => p.PublishedDate)
                .ToList();

            ViewBag.PageSize = 9;
            ViewBag.TotalBookReviews = tourPosts.Count;
            return View(tourPosts);
        }

        // GET: TourController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TourController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TourController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TourController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TourController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TourController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TourController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
