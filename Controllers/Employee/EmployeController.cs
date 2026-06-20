using Microsoft.AspNetCore.Mvc;
using Star_Project.Controllers.User;
using Star_Project.Database;
using Star_Project.Models;

namespace Star_Project.Controllers.Employee
{
    public class EmployeController : Controller
    {
        private readonly Db dt;
        public EmployeController(Db dt)
        {
            this.dt = dt;
        }
        public IActionResult Index()
        {
            return RedirectToAction("MyApplications");
        }

        [HttpGet]
        public IActionResult MyApplications()
        {
           var data = dt.Contacts.ToList();
            return View(data);
        }
        public IActionResult Delete(int id)
        {
            // adminreg table se find karein, Contacts se nahi
            var data = dt.Contacts.Find(id);  // ← YEH CHANGE KIYA

            if (data != null)
            {
                dt.Contacts.Remove(data);      // ← YEH CHANGE KIYA
                dt.SaveChanges();
            }

            return RedirectToAction("MyApplications");
        }
    }
}
