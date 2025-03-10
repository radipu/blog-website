using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Data;

namespace My_Blog_Website.Controllers.Categories
{
    public class CSharpASPNETController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CSharpASPNETController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var csharpPosts = _context.posts
                .Where(p => p.Categories == "C# & ASP.NET")  // Adjust to your actual category property name
                .OrderByDescending(p => p.PublishedDate)
                .ToList();

            return View(csharpPosts);
        }

        // GET: CSharpASPNETController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CSharpASPNETController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CSharpASPNETController/Create
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

        // GET: CSharpASPNETController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CSharpASPNETController/Edit/5
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

        // GET: CSharpASPNETController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CSharpASPNETController/Delete/5
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
