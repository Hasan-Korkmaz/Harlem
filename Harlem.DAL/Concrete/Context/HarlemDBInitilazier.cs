using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harlem.DAL.Concrete.Context
{
   public  static class HarlemDBInitilazier 
    {
        //Ef Core DB İnit
        public static bool   SeedData(DbContext context)
        {
            List<Category> seedCategory =new List<Category>();
            List<Product> seedProduct =new List<Product>();
            if (!context.Set<Category>().Any())
            {
                seedCategory.Add(new Category()
                { 
                    Id = Guid.NewGuid(),
                    Name = "Telefon",
                    DisplayName = "Telefon",
                    InsertDateTime = DateTime.Now, 
                    isActive=true,
                    isDelete=false,
                    Description="Telefon ve Benzeri",});
                seedCategory.Add(new Category() {
                    Id = Guid.NewGuid(),
                    Name = "Bilgisayar",
                    DisplayName = "Bilgisayar",
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false,
                    Description = "Bilgisayar ve Benzeri",
                });
            }
            if (!context.Set<Product>().Any())
            {
                seedProduct.Add(new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Xiaomi Red Mi Note 8 Pro",
                    InsertDateTime = DateTime.Now,
                    Category=seedCategory[0],
                    Price=2300,
                    ProductDetail="Detaylar sonra belirlenir.",
                    StockType=Enums.StockType.Adet,
                    StockPiece=5,
                    isActive = true,
                    isDelete = false,
                });
             
            }
            context.AddRange(seedCategory);
            context.AddRange(seedProduct);
            context.SaveChanges();


            return true;
        }
        
    }
}
