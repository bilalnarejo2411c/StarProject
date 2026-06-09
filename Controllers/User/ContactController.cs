using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Star_Project.Database;
using Star_Project.Models;
using System.Net.Mail;

namespace Star_Project.Controllers.User
{
    public class ContactController : Controller
    {
        private readonly Db _context;
        private readonly IConfiguration _config;

        public ContactController(Db context, IConfiguration config)
        {
            _context = context;
            _config = config;
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

            // Database mein save karo
            _context.Contacts.Add(contact);
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
                message.To.Add(new MailboxAddress(contact.FirstName, contact.Email));
                message.Subject = "Thank you for contacting Star Securities!";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>Shukriya, {contact.FirstName}!</h2>
                        <p>Aapki inquiry humein mil gayi hai.</p>
                        <p>Hum jald hi aapse rabta karenge.</p>
                        <br/>
                        <p><b>Star Securities Team</b></p>
                    "
                };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(senderEmail, senderPass);
                smtp.Send(message);
                smtp.Disconnect(true);

                TempData["Success"] = "Aapki inquiry submit ho gayi! Confirmation email bhej diya gaya.";
            }
            catch (Exception ex)
            {
                // Email fail ho toh bhi form submit ho
                TempData["Success"] = "Aapki inquiry submit ho gayi!";
                Console.WriteLine("Email Error: " + ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}