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
            //All Fake Data 
            List<Category> seedCategory =new List<Category>();
            List<Product> seedProduct =new List<Product>();
            List<AccountUser> seedAccountUser =new List<AccountUser>();
            List<User> seedMenagementUser =new List<User>();
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
                seedCategory.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Moda",
                    DisplayName = "Moda",
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false,
                    Description = "Kadın Giyim, Erkek Giyim",
                });
                seedCategory.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Ev Bahçe Ofis Market",
                    DisplayName = "Ev Bahçe Ofis Market",
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false,
                    Description = "Ev Bahçe Ofis Market Eviniz İçin En iyi Dekor Malzemesi ve Daha Fazlası",
                });
                seedCategory.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Kozmetik",
                    DisplayName = "Kozmetik",
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false,
                    Description = "Güzellik, Makyaj, Yüz Temizleme",
                });
                seedCategory.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Süper Market",
                    DisplayName = "Süper Market",
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false,
                    Description = "Yiyecek, İçecek, Oyuncak, Şarküteri, Et & Süt Ürünleri",
                });
                seedCategory.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Kitap & Müzik",
                    DisplayName = "Kitap & Müzik",
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false,
                    Description = "Best Seller, 2020 Hitleri, K-POP ve Daha Fazlası",
                });
            }
            if (!context.Set<Product>().Any())
            {
                var productId = Guid.Parse("6bc5703e-743c-434f-9008-8559bf1f7597");
                var productId2 = Guid.Parse("FFB012E1-9607-4713-9C68-E1C118F1F2A1");
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
                seedProduct.Add(new Product()
                {
                    Id = productId2,
                    Name = "Lenovo Idepad 700",
                    InsertDateTime = DateTime.Now,
                    Category = seedCategory[1],
                    Price = 12500.99m,
                    ProductDetail = "Lenovo İdepad 700 benim kullandığım canavar bir bilgisayardır ",
                    StockType = Enums.StockType.Adet,
                    StockPiece = 3,
                    isActive = true,
                    isDelete = false,
                    ProductImages = new List<ProductImage>()
                });
           
                seedProduct[0].ProductImages.Add(new ProductImage()
                {
                    Id = Guid.NewGuid(),
                    ImageName = "Seed Value",
                    ImageAltValue = "Xiaomi Resim 1",
                    PhysicalName = "a57395b3-ca9d-49d8-855e-24ad656ee884.jpg",
                    PhysicalPath = @"C:\Users\HasanKorkmaz\source\repos\Harlem\Harlem.Web\wwwroot\Images\Product\6bc5703e-743c-434f-9008-8559bf1f7597\a57395b3-ca9d-49d8-855e-24ad656ee884.jpg",
                    PublicURL = @"/Images/Product/6bc5703e-743c-434f-9008-8559bf1f7597/a57395b3-ca9d-49d8-855e-24ad656ee884.jpg",
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
                    PublicURL = @"/Images/Product/6bc5703e-743c-434f-9008-8559bf1f7597/76583b8c-56d5-455f-b244-ea76d62e6f28.jpg",
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
                    PublicURL = @"/Images/Product/6bc5703e-743c-434f-9008-8559bf1f7597/7C6D67CB-FEAF-49EB-81CC-8DAB1F3B0B5A.png",
                    ProductId = productId,
                    Order = 3,
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false

                });
                seedProduct[1].ProductImages.Add(new ProductImage()
                {
                    Id = Guid.NewGuid(),
                    ImageName = "Seed Value",
                    ImageAltValue = "Lenovo İdepad Test Resim ",
                    PhysicalName = "CEA1196C-9454-400A-9E37-8C2EE630B97B.jpg",
                    PhysicalPath = @"C:\Users\HasanKorkmaz\source\repos\Harlem\Harlem.Web\wwwroot\Images\Product\ffb012e1-9607-4713-9c68-e1c118f1f2a1\CEA1196C-9454-400A-9E37-8C2EE630B97B.jpg",
                    PublicURL = @"/Images/Product/ffb012e1-9607-4713-9c68-e1c118f1f2a1/CEA1196C-9454-400A-9E37-8C2EE630B97B.jpg",
                    ProductId = productId2,
                    Order = 1,
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false

                });
                seedProduct[1].ProductImages.Add(new ProductImage()
                {
                    Id = Guid.NewGuid(),
                    ImageName = "Seed Value",
                    ImageAltValue = "Lenovo İdepad Test Resim 2",
                    PhysicalName = "F96D1FBE-7242-4DE9-8FD5-D86E33243D8F.jpg",
                    PhysicalPath = @"C:\Users\HasanKorkmaz\source\repos\Harlem\Harlem.Web\wwwroot\Images\Product\ffb012e1-9607-4713-9c68-e1c118f1f2a1\F96D1FBE-7242-4DE9-8FD5-D86E33243D8F.jpg",
                    PublicURL = @"/Images/Product/ffb012e1-9607-4713-9c68-e1c118f1f2a1/F96D1FBE-7242-4DE9-8FD5-D86E33243D8F.jpg",
                    ProductId = productId2,
                    Order = 3,
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false

                });

            }
            if (!context.Set<AccountUser>().Any())
            {
                var account=new AccountUser()
                {
                    Id = Guid.NewGuid(),
                    Name = "Celal",
                    Surname = "Şengör",
                    Password = "customer".Md5Hash(),
                    Phone = "+905003529090",
                    Email = "CDelta@Harlemticaret.com",
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false,
                    Addresses=new List<AccountUserAddress>()
                };
                account.Addresses.Add(new AccountUserAddress()
                {
                    Id = Guid.NewGuid(),
                    Name = "Ev Adresi",
                    AddressDetail = "Şair Ziya Paşa Cad. Akgün iş hanı No:37 D:12 Karaköy - İSTANBUL / TURKEY, Beyoğlu",
                    isActive = true,
                    isDelete = false,
                    InsertDateTime = DateTime.Now
                });
                account.Addresses.Add(new AccountUserAddress()
                {
                    Id = Guid.NewGuid(),
                    Name = "İş Adresi",
                    AddressDetail = "Hacımimi, Karanlık Fırın Sk. No:5, 34425 Beyoğlu/İstanbul",
                    isActive = true,
                    isDelete = false,
                    InsertDateTime = DateTime.Now
                });
                account.Addresses.Add(new AccountUserAddress()
                {
                    Id = Guid.NewGuid(),
                    Name = "Plaza Adresi",
                    AddressDetail = "Müeyyedzade, Kemeraltı Caddesi, Ümmehan 17/5, 34425 Beyoğlu/İstanbul",
                    isActive = true,
                    isDelete = false,
                    InsertDateTime = DateTime.Now
                });
                seedAccountUser.Add(account);
            }
            if (!context.Set<User>().Any())
            {
                seedMenagementUser.Add( new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nuri",
                    Surname = "Aktekin",
                    Password = "admin".Md5Hash(),
                    Email = "admin@harlemticaret.com",
                    InsertDateTime = DateTime.Now,
                    isActive = true,
                    isDelete = false,
                });
            }
            context.AddRange(seedCategory);
            context.AddRange(seedProduct);
            context.AddRange(seedAccountUser);
            context.AddRange(seedMenagementUser);
            context.SaveChanges();

            return true;
        }
        
    }
}
