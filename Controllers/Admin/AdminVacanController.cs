using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;
using Star_Project.Models;

namespace Star_Project.Controllers.Admin
{
    public class AdminVacanController : Controller
    {

       private readonly Db dt;
        public AdminVacanController(Db dt)
        {
            this.dt = dt;
        }
        // GET: AdminVacanController
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Index(AdminVaccanies a)
        {
           var data = dt.Vacancies.ToList();
            return View(data);
        }




        // GET: AdminVacanController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminVacanController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminVacanController/Create
        [HttpPost]
        public IActionResult Create(AdminVaccanies a)
        {
            if (ModelState.IsValid)
            {
                dt.Vacancies.Add(a);
                dt.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(a);
        }
        // GET: AdminVacanController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminVacanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        // GET: AdminVacanController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminVacanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
