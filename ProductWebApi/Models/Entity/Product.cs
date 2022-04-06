using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Models.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
        public string Unit { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Quantity { get; set; }
        public string ShortDescription { get; set; }
        public int CategoryId { get; set; }
        public int ImageId { get; set; }   

    }
}
