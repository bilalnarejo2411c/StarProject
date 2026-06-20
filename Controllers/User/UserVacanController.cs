using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Star_Project.Database;

namespace Star_Project.Controllers.User
{
    public class UserVacanController : Controller
    {
        Db dt;
        public UserVacanController(Db dt)
        {
            this.dt = dt;
        }
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
