using Microsoft.AspNetCore.Mvc;

namespace Star_Project.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
