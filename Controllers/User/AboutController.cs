using Microsoft.AspNetCore.Mvc;

namespace Star_Project.Controllers.User
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
