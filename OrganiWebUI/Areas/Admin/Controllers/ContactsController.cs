using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Organi.Domain.DataContext;
using Organi.Domain.Entity;
using Organi.Domain.ViewModel;

namespace OrganiWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly OrganiDbContext _context;
        readonly IConfiguration conf;

        public ContactsController(OrganiDbContext context, IConfiguration conf)
        {
            this.conf = conf;
            _context = context;
        }

        // GET: Admin/Contacts
        [HttpGet]
        public IActionResult Index(
            int pageIndex = 1,
            int pageSize = 3)
        {

            /*if (typeId.HasValue && typeId == 1)
            {
                query = query.Where(c => c.Answer == null);
            }*/
            var query = _context.Contacts
                .OrderByDescending(c => c.Id);
            var model = new PagedViewModel(query, pageIndex, pageSize);

            return View(model);
        }

        // Post: Admin/Contacts/Answer/5
        public async Task<IActionResult> Answer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            if(contact.Answer != null)
            {
                return BadRequest();
            }
            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answer(int id,[Bind("Id,Answer ")]Contact contact)
        {
            try
            {
                if (id != contact.Id)
                {
                    return NotFound();
                }

                var entity = await _context.Contacts
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (entity == null)
                {
                    return NotFound();
                }
                if (entity.Answer != null)
                {
                    return NotFound();
                }

                entity.Answer = contact.Answer;
                entity.AnswerDate = DateTime.UtcNow.AddHours(4);

                var host = conf.GetSection("emailAccount").GetSection("server").Value;
                var port = int.Parse(conf.GetSection("emailAccount").GetSection("port").Value);
                var userName = conf.GetSection("emailAccount").GetSection("userName").Value;
                var password = conf.GetSection("emailAccount").GetSection("password").Value;
                //string[] cc = conf.GetSection("emailAccount").GetSection("cc").Value
                //    .Split(";",StringSplitOptions.RemoveEmptyEntries);
                SmtpClient client = new SmtpClient(host, port);
                client.Credentials = new NetworkCredential(userName, password);
                client.EnableSsl = true;
                MailMessage message = new MailMessage(userName, entity.Email, "Organi Admin", entity.Answer);
                message.IsBodyHtml = true;
                //foreach (var address in cc)
                //{
                //    message.CC.Add(address);
                //}
                client.Send(message);

                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new Exception("hey");
            }
            
        }
    }
}
