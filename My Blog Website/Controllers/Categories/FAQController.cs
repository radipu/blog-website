using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Data;
using My_Blog_Website.Models;
using System.Text.Json;

namespace My_Blog_Website.Controllers.Categories
{
    public class FAQController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FAQController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region client side
        [HttpGet]
        public ActionResult Index()
        {
            // Enhanced null filtering
            var faqs = _context.faqs
                .Where(f => f.FAQanswer != null && !string.IsNullOrWhiteSpace(f.FAQanswer))
                .ToList();

            return View(faqs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitQuestion([FromBody] FAQs faq)
        {
            if (string.IsNullOrWhiteSpace(faq.FAQuestion))
            {
                return BadRequest("Question cannot be empty");
            }

            try
            {
                faq.FAQanswer = null; // Ensure answer is null
                _context.faqs.Add(faq);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving question: {ex.Message}");
            }
        }

        #endregion client side

        #region admin side
        [HttpGet]
        [Route("admin/faq")]
        public ActionResult AdminIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUnansweredQuestions()
        {
            try
            {
                // Raw SQL query for testing
                var questions = _context.faqs
                    .FromSqlRaw("SELECT FAQid, FAQuestion, FAQanswer FROM faqs WHERE FAQanswer IS NULL")
                    .Select(f => new
                    {
                        FAQid = f.FAQid,
                        FAQuestion = f.FAQuestion
                    })
                    .ToList();

                Console.WriteLine($"Found {questions.Count} unanswered questions");
                return Json(questions, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex}");
                return StatusCode(500, new { error = ex.ToString() });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFAQ(int id)
        {
            var faq = await _context.faqs.FindAsync(id);
            return faq != null ? Json(faq) : NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveQnA([FromBody] FAQs faq)
        {
            try
            {
                if (faq.FAQid == 0) // New QnA
                {
                    if (string.IsNullOrWhiteSpace(faq.FAQuestion) || string.IsNullOrWhiteSpace(faq.FAQanswer))
                    {
                        return BadRequest("Both question and answer are required for new entries");
                    }
                    _context.faqs.Add(faq);
                }
                else // Existing QnA
                {
                    var existing = await _context.faqs.FindAsync(faq.FAQid);
                    if (existing == null) return NotFound();

                    existing.FAQuestion = faq.FAQuestion;
                    existing.FAQanswer = faq.FAQanswer;
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving: {ex.Message}");
            }
        }

        #endregion admin side

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
