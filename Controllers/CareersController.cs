using Microsoft.AspNetCore.Mvc;

namespace Star_Project.Controllers
{
    public class CareersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
