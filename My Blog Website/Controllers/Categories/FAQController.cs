using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Data;
using My_Blog_Website.Models;

namespace My_Blog_Website.Controllers.Categories
{
    public class FAQController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FAQController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var faqs = _context.faqs.ToList();
            return View(faqs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitQuestion([FromBody] FAQs faq)
        {
            if (ModelState.IsValid)
            {
                // Ensure the answer is null for a new user-submitted question
                faq.FAQanswer = null;
                _context.faqs.Add(faq);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return BadRequest(ModelState);
        }


        [HttpGet]
        [Route("admin/faq")]
        public ActionResult AdminIndex()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/FAQs/SaveQnA")] // Add proper routing
        public async Task<IActionResult> SaveQnA([FromBody] FAQs faq) // Add [FromBody]
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.faqs.Add(faq); // Fix capitalization
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    // Log the error
                    return StatusCode(500, $"Database error: {ex.Message}");
                }
            }
            return BadRequest(ModelState);
        }

        // GET: FAQController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FAQController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FAQController/Create
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

        // GET: FAQController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FAQController/Edit/5
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

        // GET: FAQController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FAQController/Delete/5
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
