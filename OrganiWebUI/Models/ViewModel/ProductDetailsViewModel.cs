using Microsoft.AspNetCore.Mvc;
using Organi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organi.Domain.ViewModel
{
    public class ProductDetailsViewModel
    {
        public Product Currrent { get; set; }
        public List<Product> RelatedProducts { get; set; }
    }
}
