using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_Blog_Website.Controllers.Categories
{
    public class ThoughtsController : Controller
    {
        // GET: ThoughtsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ThoughtsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ThoughtsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThoughtsController/Create
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

        // GET: ThoughtsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ThoughtsController/Edit/5
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

        // GET: ThoughtsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ThoughtsController/Delete/5
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
