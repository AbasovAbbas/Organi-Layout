using DemoWebApi.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static List<Contact> Contacts = new List<Contact>
        {
            new Contact
            {
                Id = 1,
                Name = "ramin",
                Number = "1234"
            },
            new Contact
            {
                Id = 2,
                Name = "murad",
                Number = "1234"
            }
        };

        public IActionResult Get()
        {
            return Ok(Contacts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contact = Contacts.FirstOrDefault(c => c.Id == id);
            return Ok(contact);
        }

        [HttpPost]
        public IActionResult Post(Contact contact)
        {
            Contacts.Add(contact);
            return Ok(contact);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Contact contact)
        {
            var entry = Contacts.FirstOrDefault(contact => contact.Id == id);
            entry.Name = contact.Name;
            entry.Number = contact.Number;
            return Ok(entry);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entry = Contacts.FirstOrDefault(contact => contact.Id == id);
            Contacts.Remove(entry);
            return Ok(entry);
        }
    }
}
