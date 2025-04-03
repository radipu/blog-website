using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace My_Blog_Website.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]
    [Route("admin/dashboard")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
