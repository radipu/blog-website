using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Data;

namespace My_Blog_Website.Controllers.Categories
{
    public class BookReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var bookReviews = _context.posts
                .Where(p => p.Categories == "Book-Review" && p.PostStatus == "Published")
                .OrderByDescending(p => p.PublishedDate)
                .ToList();

            ViewBag.PageSize = 9;
            ViewBag.TotalBookReviews = bookReviews.Count;

            return View(bookReviews);
        }

        // GET: BookReviewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookReviewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookReviewController/Create
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

        // GET: BookReviewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookReviewController/Edit/5
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

        // GET: BookReviewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookReviewController/Delete/5
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
