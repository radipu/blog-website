using Microsoft.AspNetCore.Mvc;

namespace My_Blog_Website.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
