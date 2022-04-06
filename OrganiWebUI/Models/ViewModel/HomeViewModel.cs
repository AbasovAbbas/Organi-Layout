using Organi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organi.Domain.ViewModel
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> FeateredProducts { get; set; }
    }
}
