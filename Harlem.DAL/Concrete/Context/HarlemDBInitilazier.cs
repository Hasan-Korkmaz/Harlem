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
                var productId = Guid.Parse("6bc5703e-743c-434f-9008-8559bf1f7597");
                seedProduct.Add(new Product()
                {
                    Id = productId,
                    Name = "Xiaomi Red Mi Note 8 Pro",
                    InsertDateTime = DateTime.Now,
                    Category=seedCategory[0],
                    Price=2300,
                    ProductDetail="Detaylar sonra belirlenir.",
                    StockType=Enums.StockType.Adet,
                    StockPiece=5,
                    isActive = true,
                    isDelete = false,
                    ProductImages=new List<ProductImage>()
                });
                seedProduct[0].ProductImages.Add(new ProductImage()
                {
                    Id = Guid.NewGuid(),
                    ImageName = "Seed Value",
                    ImageAltValue = "Xiaomi Resim 1",
                    PhysicalName = "a57395b3-ca9d-49d8-855e-24ad656ee884.jpg",
                    PhysicalPath = @"C:\Users\HasanKorkmaz\source\repos\Harlem\Harlem.Web\wwwroot\Images\Product\6bc5703e-743c-434f-9008-8559bf1f7597\a57395b3-ca9d-49d8-855e-24ad656ee884.jpg",
                    PublicURL = @"\Images\Product\6bc5703e-743c-434f-9008-8559bf1f7597\a57395b3-ca9d-49d8-855e-24ad656ee884.jpg",
                    ProductId = productId,
                    Order = 1,
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false

                });
                
                seedProduct[0].ProductImages.Add(new ProductImage()
                {
                    Id = Guid.NewGuid(),
                    ImageName = "Seed Value",
                    ImageAltValue = "Xiaomi Resim 2",
                    PhysicalName = "76583b8c-56d5-455f-b244-ea76d62e6f28.jpg",
                    PhysicalPath = @"C:\Users\HasanKorkmaz\source\repos\Harlem\Harlem.Web\wwwroot\Images\Product\6bc5703e-743c-434f-9008-8559bf1f7597\76583b8c-56d5-455f-b244-ea76d62e6f28.jpg",
                    PublicURL = @"\Images\Product\6bc5703e-743c-434f-9008-8559bf1f7597\76583b8c-56d5-455f-b244-ea76d62e6f28.jpg",
                    ProductId = productId,
                    Order = 2,
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false

                });
                seedProduct[0].ProductImages.Add(new ProductImage()
                {
                    Id = Guid.NewGuid(),
                    ImageName = "Seed Value",
                    ImageAltValue = "Xiaomi Resim 3",
                    PhysicalName = "7C6D67CB-FEAF-49EB-81CC-8DAB1F3B0B5A.png",
                    PhysicalPath = @"C:\Users\HasanKorkmaz\source\repos\Harlem\Harlem.Web\wwwroot\Images\Product\6bc5703e-743c-434f-9008-8559bf1f7597\7C6D67CB-FEAF-49EB-81CC-8DAB1F3B0B5A.png",
                    PublicURL = @"\Images\Product\6bc5703e-743c-434f-9008-8559bf1f7597\7C6D67CB-FEAF-49EB-81CC-8DAB1F3B0B5A.png",
                    ProductId = productId,
                    Order = 3,
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false

                });

            }
            context.AddRange(seedCategory);
            context.AddRange(seedProduct);
            context.SaveChanges();


            return true;
        }
        
    }
}
