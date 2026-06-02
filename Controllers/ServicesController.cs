using Microsoft.AspNetCore.Mvc;

namespace Star_Project.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
