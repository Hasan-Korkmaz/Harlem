using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Harlem.Entity.FrontEndTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harlem.Web.Areas.backoffice.Controllers
{
    [Area("BackOffice")]
    public class GeneralController : Controller
    {
        public ICategoryService CategoryService{ get; set; }
        public GeneralController(ICategoryService categoryService)
        {
            if (CategoryService==null)
            {
                this.CategoryService = categoryService;
            }
        }

        [HttpGet]
        public ApiResponse<List<Select2>> GetCategories(string key,Guid Id )
        {
           var bllResult= this.CategoryService.GetAll(x => x.isActive == true && (string.IsNullOrEmpty(key) ||  x.DisplayName.Contains(key)) && (Id==Guid.Empty || x.Id==Id));
            if (bllResult.Status==Enums.BLLResultType.Success)
            {
              var evolvedData=  bllResult.Entity.Select(x => new Select2() { Id = x.Id, Text = x.DisplayName }).ToList();
                return new ApiResponse<List<Select2>>() { Data = evolvedData, Message = bllResult.Message, Status = true };
            }
            else
            {
                return new ApiResponse<List<Select2>>() {   Message = bllResult.Message, Status = false };
            }
        }

        [HttpGet]
        public ApiResponse<List<Select2>> GetStockTypes(string key)
        {
            List<Select2> stockTypes = new List<Select2>();
            foreach (Enums.StockType stockType in (Enums.StockType[])Enums.StockType.GetValues(typeof(Enums.StockType)))
            {

                stockTypes.Add(new Select2() { Id = (ushort)stockType, Text = stockType.GetDescription<Enums.StockType>() });
            }
          stockTypes=  stockTypes.Where(x => (string.IsNullOrEmpty(key) || x.Text.Contains(key))).ToList();
            
                return new ApiResponse<List<Select2>>() { Data = stockTypes, Message = "", Status = true };
            
            
        }
    }
}
