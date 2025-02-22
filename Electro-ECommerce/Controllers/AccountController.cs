using Microsoft.AspNetCore.Mvc;

namespace Electro_ECommerce.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
