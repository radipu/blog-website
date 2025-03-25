using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Data;

namespace My_Blog_Website.Controllers.Categories
{
    public class IdeasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdeasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var ideaPosts = _context.posts
                .Where(p => p.Categories == "Ideas" && p.PostStatus == "Published")
                .OrderByDescending(p => p.LastModifiedDate ?? p.PublishedDate)
                .ToList();

            ViewBag.PageSize = 9;
            ViewBag.TotalBookReviews = ideaPosts.Count;
            return View(ideaPosts);
        }

        // GET: IdeasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IdeasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IdeasController/Create
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

        // GET: IdeasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IdeasController/Edit/5
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

        // GET: IdeasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IdeasController/Delete/5
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
