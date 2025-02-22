using Microsoft.AspNetCore.Mvc;

namespace Electro_ECommerce.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
