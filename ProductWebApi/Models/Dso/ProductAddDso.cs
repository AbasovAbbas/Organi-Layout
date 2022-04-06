using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Models.Dso
{
    public class ProductAddDso
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }
        public string ShortDescription { get; set; }
        public string ImageToken { get; set; }

    }
}
