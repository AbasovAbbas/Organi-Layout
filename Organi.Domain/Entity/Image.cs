using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organi.Domain.Entity
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool? IsMain { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
