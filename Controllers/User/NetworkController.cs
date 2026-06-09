using Microsoft.AspNetCore.Mvc;

namespace Star_Project.Controllers.User
{
    public class NetworkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
