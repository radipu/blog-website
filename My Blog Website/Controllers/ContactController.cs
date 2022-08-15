using Microsoft.AspNetCore.Mvc;

namespace My_Blog_Website.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
