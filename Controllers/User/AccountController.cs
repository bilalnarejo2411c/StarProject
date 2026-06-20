using System;
using System.Linq;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        // ================= LOGIN GET =================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ================= LOGIN POST =================
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

            HttpContext.Session.SetString("email", data.email);
            HttpContext.Session.SetString("Firstname", data.name);

            if (data.UserRole == "Admin")
            {
                return RedirectToAction("Home", "Admin");
            }

            if (data.UserRole == "user")
            {
                SendLoginEmail(data.email, data.name);

                return RedirectToAction("Index", "Employe");
            }

            ViewBag.Error = "Invalid User Role";
            return View();
        }

        // ================= LOGIN EMAIL =================
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

                    <p>You have successfully logged in.</p>

                    <p><b>Email:</b> {email}</p>

                    <br>

                    <p>
                        Regards,<br>
                        Star Securities Team
                    </p>"
                };

                using (var smtp = new SmtpClient())
                {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // ================= PASSWORD UPDATE EMAIL =================
        private void SendPasswordUpdateEmail(
            string email,
            string name,
            string password)
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

                message.Subject = "Account Updated";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                    <h2>Hello {name}</h2>

                    <p>Your account details have been updated.</p>

                    <p><b>Email:</b> {email}</p>
                    <p><b>Password:</b> {password}</p>

                    <br>

                    <p>
                        Regards,<br>
                        Star Securities Team
                    </p>"
                };

                using (var smtp = new SmtpClient())
                {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}