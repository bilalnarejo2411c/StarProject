using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;

namespace Star_Project.Controllers
{
    public class AdminRegistrationController : Controller
    {
        Db dt;
        public AdminRegistrationController(Db dt)
        {
            this.dt = dt;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register()
        {
            return View();
        }


    }
}
