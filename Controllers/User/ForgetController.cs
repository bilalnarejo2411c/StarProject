using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Star_Project.Database;
using Star_Project.Models;

namespace Star_Project.Controllers.User
{
    public class ForgetPasswordController : Controller
    {
        private readonly Db dt;
        private readonly IConfiguration _config;

        public ForgetPasswordController(Db dt, IConfiguration config)
        {
            this.dt = dt;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
    
        
