using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;

namespace Star_Project.Controllers.Admin
{
    public class AdminWork : Controller
    {
        Db dt;
        public AdminWork(Db dt)
        {
            this.dt = dt;
        }
        public IActionResult Index()
        {
            var data = dt.Contacts.ToList();
            return View(data);
        }
        public IActionResult Delete(int id) {
            var data = dt.Contacts.Find(id);

            if (data != null)
            {
                dt.Contacts.Remove(data);
                dt.SaveChanges();    
            }
            return RedirectToAction("Index");
        }
    }
}
