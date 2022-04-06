using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Organi.Domain.DataContext;
using Organi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OrganiWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribeController : Controller
    {
        readonly OrganiDbContext db;
        readonly IConfiguration conf;
        public SubscribeController(OrganiDbContext db, IConfiguration conf)
        {
            this.db = db;
            this.conf = conf;
        }
        public IActionResult Index()
        {
            var subscribers = db.Subcribes.ToList();
            return View(subscribers);
        }
        public IActionResult Send(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var subscribe = db.Subcribes.Find(id);
            if(subscribe == null)
            {
                return NotFound();
            }
            return View(subscribe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Send(string subject, string message, string[] emails)
        {
            var host = conf.GetSection("emailAccount").GetSection("server").Value;
            var port =int.Parse(conf.GetSection("emailAccount").GetSection("port").Value);
            var userName = conf.GetSection("emailAccount").GetSection("userName").Value;
            var password = conf.GetSection("emailAccount").GetSection("password").Value;
            //formal olaraq yadda saxlamagini istreyirik
            var contact = new Contact
            {
                Answer = message,
                Email = string.Join(';', emails),
                Name = "Organi subscribers"
            };
            db.Contacts.Add(contact);
            db.SaveChanges();
            SmtpClient client = new SmtpClient(host, port);
            client.Credentials = new NetworkCredential(userName, password);
            client.EnableSsl = true;

            foreach (var email in emails)
            {
                using( MailMessage mailMessage = new MailMessage(userName, email))
                {
                    mailMessage.Subject = "Organi Admin";
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = true;

                    client.Send(mailMessage);
                }
               
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
