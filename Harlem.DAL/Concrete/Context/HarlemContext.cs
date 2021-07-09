using Harlem.Entity.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Harlem.DAL.Concrete.Context
{
    public class HarlemContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=HarlemDB;Trusted_Connection=True;");
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
        public DbSet<ProductImage> ProductImages{ get; set; }


        // Account User -> Customer

        public DbSet<AccountUser> AccountUser { get; set; }
        public DbSet<AccountUserAddress> AccountUsersAdress { get; set; }

        // Order

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        // Basket
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }

        // User -> Admin User

        public DbSet<User> User { get; set; }
    }
}
