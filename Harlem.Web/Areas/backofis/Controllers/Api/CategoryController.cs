using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.Entity.DbModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harlem.Web.Areas.backofis.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]

    public class CategoryController : ControllerBase
    {
        ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [Route("Add")]
        [HttpPost]
        public ApiResponse<Category> Add(Category category)
        {
            var resp = categoryService.Add(category);
            return new ApiResponse<Category>() { Data = null, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }
        [HttpPost]
        public ApiResponse<Category> Update(Category category)
        {
            var resp = categoryService.Update(category);
            return new ApiResponse<Category>() { Data = resp.Entity, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }
        [HttpPut]
        public ApiResponse<Category> Delete(Guid Id)
        {
            var resp = categoryService.DeleteExpression(x => x.Id == Id);
            return new ApiResponse<Category>() { Data = resp.Entity, Message = resp.Message, Status = resp.Status == Enums.BLLResultType.Success ? true : false };
        }
    }
}
