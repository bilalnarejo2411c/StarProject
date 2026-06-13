using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Star_Project.Database;
using Star_Project.Models;

namespace Star_Project.Controllers.Admin
{
    public class AdminEmployeController : Controller
    {
        private readonly Db dt;
        private readonly IConfiguration _config;

        public AdminEmployeController(Db dt, IConfiguration config)
        {
            this.dt = dt;
            _config = config;
        }

        public IActionResult Index()
        {
            var data = dt.adminreg.ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var data = dt.adminreg.Find(id);

            if (data != null)
            {
                dt.adminreg.Remove(data);
                dt.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var data = dt.adminreg.Find(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(adminreg obj)
        {
            if (ModelState.IsValid)
            {
                dt.adminreg.Update(obj);
                dt.SaveChanges();

                // Password reset email send
                SendLoginEmail(
                    obj.email,
                    obj.name,
                    obj.password
                );

                TempData["Success"] =
                    "Employee account updated successfully and email sent.";

                return View();
            }

            return View(obj);
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

                message.Subject = "Password Reset Successfully";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>Hello {name}</h2>

                        <p>
                            Your account password has been reset successfully.
                        </p>

                        <p>
                            You can now log in and continue using our services.
                        </p>

                        <hr>

                        <p><strong>Email:</strong> {email}</p>

                        <p><strong>New Password:</strong> {password}</p>

                        <br>

                        <p>
                            Regards,<br>
                            Star Securities Team [Admin]
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