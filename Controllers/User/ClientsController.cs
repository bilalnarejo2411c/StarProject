using Microsoft.AspNetCore.Mvc;

namespace Star_Project.Controllers.User
{
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
