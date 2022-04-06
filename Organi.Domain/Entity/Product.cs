using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organi.Domain.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Image> Images { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }
        public string  Description { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]// ehtiyac yoxdu bu halda
        public virtual Category Category { get; set; }
        [NotMapped]//yani entity framework nezere almasin
        public IFormFile[] file { get; set; }
        [NotMapped]
        public int FileSelectedIndex { get; set; }
    }
}
