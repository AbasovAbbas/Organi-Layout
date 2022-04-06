using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organi.Domain.Entity
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
