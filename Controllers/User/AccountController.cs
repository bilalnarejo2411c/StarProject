using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Star_Project.Database;
using Star_Project.Models;

namespace Star_Project.Controllers.User
{
    public class AccountController : Controller
    {
        private readonly Db dt;
        private readonly IConfiguration _config;

        public AccountController(Db dt, IConfiguration config)
        {
            this.dt = dt;
            _config = config;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(adminreg a)
        {
            var data = dt.adminreg.FirstOrDefault(x =>
                x.email == a.email &&
                x.password == a.password);

            if (data == null)
            {
                
                ViewBag.Error = "Incorrect Email or Password";
                return View();
            }

            if (data.UserRole == "Admin")
            {
                return RedirectToAction("Home", "Admin");
            }

            if (data.UserRole == "user")
            {
                HttpContext.Session.SetString("email", data.email);


                SendLoginEmail(data.email, data.name);

                return RedirectToAction("Index", "Employe");
            }

            ViewBag.Error = "Invalid User Role";
            return View();
        }
       

        private void SendLoginEmail(string email, string name)
        {
            try
            {
                var emailSettings = _config.GetSection("EmailSettings");

                string smtpHost = emailSettings["SmtpHost"];
                int smtpPort = int.Parse(emailSettings["SmtpPort"]);
                string senderEmail = emailSettings["SenderEmail"];
                string senderPass = emailSettings["SenderPassword"];

                var message = new MimeMessage();

                message.From.Add(
                    new MailboxAddress("Star Securities", senderEmail));

                message.To.Add(
                    new MailboxAddress(name, email));

                message.Subject = "Login Alert";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>Hello {name}</h2>

                        <p>Your account has been logged in successfully.</p>

                        <p>
                            Login Time: {DateTime.Now:dd-MM-yyyy hh:mm tt}
                        </p>

                        <p>
                            If this login was not performed by you,
                            please contact the administrator immediately.
                        </p>

                        <br>

                        <p>
                            Regards,<br>
                            Star Securities Team
                        </p>"
                };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                smtp.Connect(
                    smtpHost,
                    smtpPort,
                    SecureSocketOptions.StartTls);

                smtp.Authenticate(
                    senderEmail,
                    senderPass);

                smtp.Send(message);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email Error: " + ex.Message);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}