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
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products{ get; set; }
    }
}
