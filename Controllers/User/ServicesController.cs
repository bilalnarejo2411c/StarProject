using Microsoft.AspNetCore.Mvc;

namespace Star_Project.Controllers.User
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
