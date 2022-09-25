using Microsoft.AspNetCore.Mvc;

namespace My_Blog_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/author")]
    public class AuthorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
