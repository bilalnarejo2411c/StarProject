using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;

namespace Star_Project.Controllers.Admin
{
    public class ApplicantsController : Controller
    {
        Db dc;
        public ApplicantsController(Db dt)
        {
            this.dc = dt;
        }
        public IActionResult Index()
        {
            var data = dc.Hirings.ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            // adminreg table se find karein, Contacts se nahi
            var data = dc.Hirings.Find(id);  // ← YEH CHANGE KIYA

            if (data != null)
            {
                dc.Hirings.Remove(data);      // ← YEH CHANGE KIYA
                dc.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
