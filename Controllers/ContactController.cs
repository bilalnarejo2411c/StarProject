using Microsoft.AspNetCore.Mvc;
using Star_Project.Models;
using Star_Project.Database;

namespace Star_Project.Controllers
{
    public class ContactController : Controller
    {
        private readonly Db _context;

        public ContactController(Db context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Something Went Wrong Try Again.";
                return View("Index", contact);
                
            }

            _context.Contacts.Add(contact);
            _context.SaveChanges();

            TempData["Success"] = "Your enquiry has been submitted successfully.";

            return RedirectToAction("Index");
        }
    }
}