using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Data;

namespace My_Blog_Website.Controllers.Categories
{
    public class HowToController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HowToController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var howtoPosts = _context.posts
                .Where(p => p.Categories == "HowTo" && p.PostStatus == "Published")
                .OrderByDescending(p => p.PublishedDate)
                .ToList();

            ViewBag.PageSize = 9;
            ViewBag.TotalBookReviews = howtoPosts.Count;
            return View(howtoPosts);
        }

        // GET: HowToController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HowToController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HowToController/Create
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

        // GET: HowToController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HowToController/Edit/5
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

        // GET: HowToController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HowToController/Delete/5
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
