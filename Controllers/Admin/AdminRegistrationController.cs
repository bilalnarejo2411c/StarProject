using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Star_Project.Database;
using Star_Project.Models;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace Star_Project.Controllers.Admin
{
    public class AdminRegistrationController : Controller
    {
        private readonly Db dt;
        private readonly IConfiguration _config;

        public AdminRegistrationController(Db dt, IConfiguration config)
        {
            this.dt = dt;
            _config = config;
        }

        [HttpGet]
        public IActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Home(adminreg a)
        {
            if (ModelState.IsValid)
            {
                dt.adminreg.Add(a);
                dt.SaveChanges();

                SendLoginEmail(a.email, a.name, a.password);

                ViewBag.Message = "Registration Successful";
            }

            return View(a);
        }
        

        private void SendLoginEmail(string email, string name, string password)
        {
            try
            {
                var emailSettings = _config.GetSection("EmailSettings");

                string smtpHost = emailSettings["SmtpHost"];
                int smtpPort = int.Parse(emailSettings["SmtpPort"]);
                string senderEmail = emailSettings["SenderEmail"];
                string senderPassword = emailSettings["SenderPassword"];

                var message = new MimeMessage();

                message.From.Add(
                    new MailboxAddress("Star Securities", senderEmail));

                message.To.Add(
                    new MailboxAddress(name, email));

                message.Subject = "Your Account Details";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                    <h2>Hello {name}</h2>

                    <p>Your account has been created successfully.</p>

                    <p><strong>Email:</strong> {email}</p>

                    <p><strong>Password:</strong> {password}</p>

                    <br>

                    <p>
                        Regards,<br>
                        Star Securities Team [Admin].
                    </p>"
                };

                using var smtp = new SmtpClient();

                smtp.Connect(
                    smtpHost,
                    smtpPort,
                    SecureSocketOptions.StartTls);

                smtp.Authenticate(
                    senderEmail,
                    senderPassword);

                smtp.Send(message);

                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email Error: " + ex.Message);
            }
        }
    }
}