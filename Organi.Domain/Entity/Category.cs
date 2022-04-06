using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organi.Domain.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFeatured { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
