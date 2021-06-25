using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO;
using Harlem.Entity.FrontEndTypes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Harlem.Web.Areas.backoffice.Controllers
{
    [Area("BackOffice")]

    public class ProductController : Controller
    {
        IProductService productService;
        IProductImageService productImageService;
        IWebHostEnvironment environment;
        public ProductController(IProductService productService, IProductImageService productImageService, IWebHostEnvironment environment)
        {
            this.productService = productService;
            this.environment = environment;
            this.productImageService = productImageService;
        }
        public IActionResult Index()
        {
            ViewBag.ActiveMenu = new ActiveMenu { ActiveSubMenu = "product", ActiveTopMenu = "defination" };

            return View();
        }
        public IActionResult Add()
        {
            ViewBag.ViewActionType = Enums.ViewStatus.Insert;
            return View("ProductInsertUpdateForm");
        }
        public IActionResult Update(Guid id)
        {
            ViewBag.ViewActionType = Enums.ViewStatus.Update;
            var model = productService.Get(x => x.Id == id);
            if (productImageService.GetAll(x => x.ProductId == id).Status==Enums.BLLResultType.Success)
            {
                model.Entity.ProductImages = productImageService.GetAll(x => x.ProductId == id).Entity;
            }

            return View("ProductInsertUpdateForm", model.Entity);
        }

        [HttpPost]
        public ApiResponse<Product> Add(Product product)
        {
            if (product.Id == Guid.Empty)
                product.Id = Guid.NewGuid();
            if (product.ProductImages!=null && product.ProductImages.Where(x=> x.Image!=null).Count()>0)
            {
                foreach (var item in product.ProductImages)
                {
                    var physicalFile = item.Image.WriteFile(environment.WebRootPath + "\\Images\\Product\\" + product.Id);
                    if (physicalFile.FileName != null)
                    {
                        if (item.Id == Guid.Empty)
                            item.Id = Guid.NewGuid();
                        item.PhysicalPath = physicalFile.PhsicalPath;
                        item.PublicURL = "\\Images\\Product\\" + product.Id + "\\" + physicalFile.FileName;
                        item.ProductId = product.Id;
                        item.InsertDateTime = DateTime.Now;
                        item.PhysicalName = physicalFile.FileName;
                        item.isActive = true;
                    }
                }
            }   
            
            var resp = productService.Add(product);
            foreach (var item in product.ProductImages)
            {
                productImageService.Add(item);
            }
            return new ApiResponse<Product>() { Data = null, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }
        [HttpPost]
        public ApiResponse<Product> Update(Product product)
        {
            var productImages = productImageService.GetAll(x => x.ProductId == product.Id);
            var newProductImages = new List<ProductImage>();
            var deleteProductImages = new List<ProductImage>();
            if (product.ProductImages != null && product.ProductImages.Where(x => x.Image != null).Count() > 0)
            {
                foreach (var item in product.ProductImages)
                {
                    //Yeni Eklenen Resimler
                    if (item.Id == Guid.Empty)
                    {
                        var physicalFile = item.Image.WriteFile(environment.WebRootPath + "\\Images\\Product\\" + product.Id);
                        if (physicalFile.FileName != null)
                        {
                            item.PhysicalPath = physicalFile.PhsicalPath;
                            item.PublicURL = "\\Images\\Product\\" + product.Id + "\\" + physicalFile.FileName;
                            item.ProductId = product.Id;
                            item.InsertDateTime = DateTime.Now;
                            item.PhysicalName = physicalFile.FileName;
                            item.isActive = true;
                            item.Id = Guid.NewGuid();
                        }
                        newProductImages.Add(item);
                    }
                    //Değiştirilen Resimler
                    else if (item.Id != Guid.Empty && item.Image != null)
                    {
                        var physicalFile = item.Image.WriteFile(environment.WebRootPath + "\\Images\\Product\\" + product.Id);
                        if (physicalFile.FileName != null)
                        {
                            item.PhysicalPath = physicalFile.PhsicalPath;
                            item.PublicURL = "\\Images\\Product\\" + product.Id + "\\" + physicalFile.FileName;
                            item.ProductId = product.Id;
                            item.InsertDateTime = DateTime.Now;
                            item.PhysicalName = physicalFile.FileName;
                            item.isActive = true;
                            item.Id = Guid.NewGuid();
                        }
                        productImageService.Update(item);
                    }
                }

                foreach (var item in product.ProductImages)
                {
                    if (item.isDelete)
                    {
                       
                            productImageService.DeleteExpression(x => x.Id == item.Id);
                        
                    }
                }
            }
            var resp = productService.Update(product);
            foreach (var item in newProductImages)
            {
                productImageService.Add(item);
            }
            foreach (var item in product.ProductImages)
            {
                if (item.isDelete)
                {
                    productImageService.DeleteExpression(x => x.Id == item.Id);
                }
            }


            resp.Entity.ProductImages = null;
            
            return new ApiResponse<Product>() { Data = resp.Entity, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }
        [HttpPut]
        public ApiResponse<Product> Delete(Guid Id)
        {
            var resp = productService.DeleteExpression(x => x.Id == Id);
            return new ApiResponse<Product>() { Data = resp.Entity, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }

        public ApiResponse<List<ProductDTO>> GetList()
        {
            //Yalnızca Kategori Tablosundaki verileri getirir dile bakmaz.
            var model = productService.GetAllDTO();
            return new ApiResponse<List<ProductDTO>>() { Data = model.Entity, Message = model.Message, Status = model.Status == Enums.BLLResultType.Success ? true : false };
        }
    }
}
