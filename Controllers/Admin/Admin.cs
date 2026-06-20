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
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login" , "Account");
        }

    }
}
