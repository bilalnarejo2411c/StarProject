using Microsoft.AspNetCore.Mvc;

namespace Star_Project.Controllers.Employee
{
    public class EmployeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
