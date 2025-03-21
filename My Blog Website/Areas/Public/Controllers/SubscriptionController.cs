using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Areas.Public.Interfaces;
using My_Blog_Website.Areas.Public.Models;
using My_Blog_Website.Data;

namespace My_Blog_Website.Areas.Public.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public SubscriptionController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Invalid email format";
                    return RedirectToAction("Index", "Home");
                }

                var existingSubscriber = await _context.subscribers
                    .FirstOrDefaultAsync(s => s.Email == email);

                if (existingSubscriber != null)
                {
                    TempData["Warning"] = "You're already subscribed!";
                    return RedirectToAction("Index", "Home");
                }

                var subscriber = new Subscriber
                {
                    Email = email,
                    SubscriptionDate = DateTime.UtcNow,
                    IsActive = true
                };

                _context.subscribers.Add(subscriber);
                await _context.SaveChangesAsync();  // Breakpoint here

                TempData["Message"] = "Subscription successful!";
            }
            catch (DbUpdateException ex)
            {
                TempData["Error"] = $"Database error: {ex.InnerException?.Message}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}