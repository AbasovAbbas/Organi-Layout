using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Organi.Domain.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string Author { get; set; }
        [Required]
        public string Message { get; set; }
        public bool Approved { get; set; }
    }
}
