using Harlem.BLL.Abstract;
using Harlem.Core.Tools;
using Harlem.DAL.Abstract;
using Harlem.Entity.DbModels;
using Harlem.Entity.DTO;
using Harlem.Entity.DTO.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Harlem.BLL.Concrete.Services
{
    public class ProductManager: TemplateDataService<Product, IProductDAL>, IProductService
    {
        public ProductManager(IProductDAL productDAL)
        {
            this._dataProvider = productDAL;
        }
        public override Result<Product> Add(Product entity)
        {
            var result = new Result<Product>();
            try
            {
                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();
                if (entity.InsertDateTime == DateTime.MinValue)
                    entity.InsertDateTime = DateTime.Now;
                entity.isDelete = false;

                result.Entity = _dataProvider.Add(entity);
                result.Status = Enums.BLLResultType.Success;
                //MagicStringler kaldırılacak.
                result.Message = "Kayıt ekleme işlemi başarılı bir şekilde gerçekleşti.";
            }
            catch (Exception ex)
            {
                //TODO : Loglama Yapılacak
                result.Status = Enums.BLLResultType.Error;
                //MagicStringler kaldırılacak.
                result.Message = @"Kayıt ekleme işlemi sırasında bir hata meydana geldi.
                                   Lütfen bu işlemi saati ile birlikte not alıp bildiriniz.";
            }
            return result;
        }


        public Result<List<ProductDTO>> GetAllDTO(Expression<Func<Product, bool>> condition)
        {
            var result = new Result<List<ProductDTO>>();
            try
            {
                result.Entity = _dataProvider.GetAllDTO(condition);

                if (result.Entity.Count > 0)
                {
                    result.Status = Enums.BLLResultType.Success;
                    //MagicStringler kaldırılacak.
                    result.Message = " Eşleşen " + result.Entity.Count + "  kayıt görüntüleniyor.";
                }
                else if (result.Entity.Count == 0)
                {
                    result.Status = Enums.BLLResultType.Empty;
                    //MagicStringler kaldırılacak.
                    result.Message = " Eşleşen kayıt bulunamadı.";
                }
                else
                {
                    result.Status = Enums.BLLResultType.Empty;
                    //MagicStringler kaldırılacak.
                    result.Message = "Arama yapılırken bir hata oluştu.";
                }

            }
            catch (Exception ex)
            {
                //TODO : Loglama Yapılacak
                result.Status = Enums.BLLResultType.Error;
                //MagicStringler kaldırılacak.
                result.Message = @"Kayıt arama işlemi sırasında bir hata meydana geldi.
                                   Lütfen bu işlemi saati ile birlikte not alıp bildiriniz.";

            }
            return result;
        }
        public  Result<List<ProductDTO>> GetWithProductImages(Expression<Func<Product, bool>> condition = null)
        {
            var result = new Result< List <ProductDTO>> ();
            try
            {
                result.Entity = _dataProvider.GetWithProductImages(condition);
                if (result.Entity != null)
                {
                    result.Status = Enums.BLLResultType.Success;
                    //MagicStringler kaldırılacak.
                    result.Message = "Eşleşen kayıt görüntüleniyor.";
                }
                else
                {
                    result.Status = Enums.BLLResultType.Empty;
                    //MagicStringler kaldırılacak.
                    result.Message = "Eşleşen kayıt bulunamadı.";
                }

            }
            catch (Exception ex)
            {
                //TODO : Loglama Yapılacak
                result.Status = Enums.BLLResultType.Error;
                //MagicStringler kaldırılacak.
                result.Message = @"Kayıt arama işlemi sırasında bir hata meydana geldi.
                                   Lütfen bu işlemi saati ile birlikte not alıp bildiriniz.";

            }
            return result;
        }

    }
}
