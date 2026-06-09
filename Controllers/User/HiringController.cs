using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Star_Project.Database;
using Star_Project.Models;

namespace Star_Project.Controllers.User
{
    public class HiringController : Controller
    {
        private readonly Db _context;
        private readonly IConfiguration _config;

        public HiringController(Db context, IConfiguration config)
        {
            _context = context;
            _config = config;
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

            // Database mein save karo
            _context.Hirings.Add(model);
            _context.SaveChanges();

            // Email bhejo
            try
            {
                var emailSettings = _config.GetSection("EmailSettings");
                string smtpHost = emailSettings["SmtpHost"];
                int smtpPort = int.Parse(emailSettings["SmtpPort"]);
                string senderEmail = emailSettings["SenderEmail"];
                string senderPass = emailSettings["SenderPassword"];

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Star Securities", senderEmail));
                message.To.Add(new MailboxAddress(model.FirstName, model.Email));
                message.Subject = "Your Job Application - Star Securities";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>Shukriya, {model.FirstName}!</h2>
                        <p>Aapki hiring request humein mil gayi hai.</p>
                        <p>Aapki application aagay pohcha di gayi hai.</p>
                        <p>Jaldi hi hum aapse rabta karenge.</p>
                        <br/>
                        <p><b>Star Securities HR Team</b></p>
                    "
                };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(senderEmail, senderPass);
                smtp.Send(message);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email Error: " + ex.Message);
            }

            TempData["Success"] = "Application Submitted Successfully! Jaldi he hum aapse rabta karenge.";
            return RedirectToAction(nameof(Index));
        }
    }
}