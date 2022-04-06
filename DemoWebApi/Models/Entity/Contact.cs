using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Models.Entity
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public override string ToString()
        {
            return $"Contact : {Id}.{Name} --- {Number}";
        }
    }
}
