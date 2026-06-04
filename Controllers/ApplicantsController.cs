using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;

namespace Star_Project.Controllers
{
    public class ApplicantsController : Controller
    {
        Db dt;
        public ApplicantsController(Db dt)
        {
            this.dt = dt;
        }
        public IActionResult Index()
        {
            var data = dt.Hirings.ToList();
            return View(data);
        }


    }
}
