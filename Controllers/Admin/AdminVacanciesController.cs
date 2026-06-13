using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;
using Star_Project.Models;

namespace Star_Project.Controllers.Admin
{
    public class AdminVacanciesController : Controller
    {
        Db dt;
        public AdminVacanciesController(Db dt)
        {
            this.dt = dt;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(AdminVaccanies a)
        {
            var data = dt.Vacancies.Add(a);
                if (ModelState.IsValid)
            {
                dt.SaveChanges();
                return RedirectToAction("Index" , "AdminVacancies");

            }
            ViewBag("Error");
            return View();
        }
    }
}
