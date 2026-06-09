using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;


namespace Star_Project.Controllers.Admin
{
    public class Admin : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

    }
}
