using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Data;
using My_Blog_Website.Models;
using System.Text.Json;

namespace My_Blog_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FAQAnswer : Controller
    {
        private readonly ApplicationDbContext _context;

        public FAQAnswer(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("admin/faq")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("admin/faq/GetUnansweredQuestions")]
        public IActionResult GetUnansweredQuestions()
        {
            try
            {
                // Raw SQL query for testing
                var questions = _context.faqs
                    .FromSqlRaw("SELECT FAQid, FAQuestion, FAQanswer FROM dbo.faqs WHERE FAQanswer IS NULL") // Explicit schema
                    .Select(f => new
                    {
                        f.FAQid,
                        f.FAQuestion
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
        [Route("admin/faq/GetFAQ")]
        public async Task<IActionResult> GetFAQ(int id)
        {
            try
            {
                Console.WriteLine($"Attempting to fetch FAQ ID: {id}");

                var faq = await _context.faqs
                    .FirstOrDefaultAsync(f => f.FAQid == id);

                if (faq == null)
                {
                    Console.WriteLine($"FAQ ID {id} not found in database");
                    return NotFound(new { error = "Question not found" });
                }

                Console.WriteLine($"Found FAQ: {faq.FAQid} | {faq.FAQuestion}");
                return Json(new
                {
                    FAQid = faq.FAQid,
                    FAQuestion = faq.FAQuestion
                }, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CRITICAL ERROR in GetFAQ:");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpPost]
        [Route("admin/faq/SaveQnA")]
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
    }
}