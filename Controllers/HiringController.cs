using Microsoft.AspNetCore.Mvc;
using Star_Project.Database;
using Star_Project.Models;

namespace Star_Project.Controllers
{
    public class HiringController : Controller
    {
        private readonly Db _context;

        public HiringController(Db context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(Hiring model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            _context.Hirings.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Application Submitted Successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}