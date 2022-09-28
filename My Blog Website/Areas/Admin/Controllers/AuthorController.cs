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

        [HttpGet]
        [Route("admin/author/edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var author = _context.authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [Route("admin/author/edit/{id}")]
        public async Task<IActionResult> Edit(Authors author)
        {
            if (ModelState.IsValid)
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        [HttpGet]
        [Route("admin/author/delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var author = _context.authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [Route("admin/author/delete/{id}")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var author = _context.authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            _context.authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
