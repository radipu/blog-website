using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Data;

namespace My_Blog_Website.Controllers.Categories
{
    public class TechnologyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechnologyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var technologyPosts = _context.posts
                .Where(p => p.Categories == "Technology" && p.PostStatus == "Published")
                .OrderByDescending(p => p.PublishedDate)
                .ToList();

            ViewBag.PageSize = 9;
            ViewBag.TotalBookReviews = technologyPosts.Count;
            return View(technologyPosts);
        }

        // GET: TechnologyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TechnologyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TechnologyController/Create
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

        // GET: TechnologyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TechnologyController/Edit/5
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

        // GET: TechnologyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TechnologyController/Delete/5
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
