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
    }
}
