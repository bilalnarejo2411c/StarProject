using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;


namespace Star_Project.Controllers
{
    public class AdminController : Controller
    {

        //Db dt;
        //public AdminController (Db dt)
        //{
        //    this.dt = dt;
        //}

        public IActionResult Home()
        {
            return View();
        }
    }
}
