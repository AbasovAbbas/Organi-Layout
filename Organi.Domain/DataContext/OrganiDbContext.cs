using Microsoft.EntityFrameworkCore;
using Organi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organi.Domain.DataContext
{
    public class OrganiDbContext : DbContext
    {
        public OrganiDbContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppInfo> AppInfos { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Subscribe> Subcribes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(cfg =>
            { 
                cfg.Property(p => p.CreatedDate).HasDefaultValueSql("dateadd(hour,4,getutcdate())");
            });
            
            
        }
    }
}
