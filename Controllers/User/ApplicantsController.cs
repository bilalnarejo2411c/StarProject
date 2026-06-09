using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;

namespace Star_Project.Controllers.User
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
            var data = dc.Hirings.Find(id);

            if (data == null)
            {
                return NotFound();
            }

            dc.Hirings.Remove(data);
            dc.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
