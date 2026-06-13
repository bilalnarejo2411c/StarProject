using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;

namespace Star_Project.Controllers.Admin
{
    public class AdminWorkController : Controller
    {
        Db dt;
        public AdminWorkController(Db dt)
        {
            this.dt = dt;
        }
        public IActionResult Index()
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

            return RedirectToAction("Index");
        }
    }
}
