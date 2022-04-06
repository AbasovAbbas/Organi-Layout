using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Models.Entity
{
    public class Image
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string FileName { get; set; }
    }
}
