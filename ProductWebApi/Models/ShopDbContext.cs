using Microsoft.EntityFrameworkCore;
using ProductWebApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Models
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions options) 
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }


    }
}
