using Harlem.Entity.DbModels;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.DAL.Concrete.Context
{
   public class HarlemContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=HarlemDB;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
          .HasOne(i => i.Address)
          .WithMany(c => c.Orders)
          .OnDelete(DeleteBehavior.NoAction)
          ;
        }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductImage> ProductImagess{ get; set; }
    }
}
