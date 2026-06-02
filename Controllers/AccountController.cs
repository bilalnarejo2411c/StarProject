using Microsoft.AspNetCore.Mvc;

namespace Star_Project.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
