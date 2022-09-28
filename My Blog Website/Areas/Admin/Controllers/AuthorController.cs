using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Areas.Admin.Models;
using My_Blog_Website.Data;

namespace My_Blog_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuthorController(ApplicationDbContext context)
        {
            _context = context;                
        }

        [HttpGet]
        [Route("admin/author")]
        public IActionResult Index()
        {
            List<Authors> authors = _context.authors.ToList();
            return View(authors);
        }

        [HttpGet]
        [Route("admin/author/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/author/create")]
        public async Task<IActionResult> Create(Authors author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(author);
        }
    }
}
