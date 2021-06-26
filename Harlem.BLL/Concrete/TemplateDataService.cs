using Harlem.Core.Tools;
using Harlem.DAL.Abstract.Template;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Harlem.BLL.Concrete
{
    public class TemplateDataService<TEntity, TDataProvider> : IService where TEntity : BaseEntity where TDataProvider : IDataAccesTemplate<TEntity>
    {
        //DataAccesLayer Instance
        internal TDataProvider _dataProvider;
        public virtual Result<TEntity> Add(TEntity entity)
        {
            var result = new Result<TEntity>();
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
        public virtual Result<TEntity> Update(TEntity entity)
        {
            var result = new Result<TEntity>();
            try
            {
               
                    entity.UpdateDateTime = DateTime.Now;
                result.Entity = _dataProvider.Update(entity);
                result.Status = Enums.BLLResultType.Success;
                //MagicStringler kaldırılacak.
                result.Message = "Kayıt güncelleme işlemi başarılı bir şekilde gerçekleşti.";
            }
            catch (Exception ex)
            {
                //TODO : Loglama Yapılacak
                result.Status = Enums.BLLResultType.Error;
                //MagicStringler kaldırılacak.
                result.Message = @"Kayıt güncelleme işlemi sırasında bir hata meydana geldi.
                                   Lütfen bu işlemi saati ile birlikte not alıp bildiriniz.";
            }
            return result;
        }
        public virtual Result<TEntity> Delete(TEntity entity)
        {
            var result = new Result<TEntity>();
            try
            {
                entity.DeletedDateTime = DateTime.Now;
                entity.isDelete = true;
                _dataProvider.Delete(entity);
                result.Status = Enums.BLLResultType.Success;
                //MagicStringler kaldırılacak.
                result.Message = "Kayıt silme işlemi başarılı bir şekilde gerçekleşti.";
            }
            catch (Exception ex)
            {
                //TODO : Loglama Yapılacak
                result.Status = Enums.BLLResultType.Error;
                //MagicStringler kaldırılacak.
                result.Message = @"Kayıt silme işlemi sırasında bir hata meydana geldi.
                                   Lütfen bu işlemi saati ile birlikte not alıp bildiriniz.";

            }
            return result;
        }
        public virtual Result<TEntity> DeleteExpression(Expression<Func<TEntity, bool>> condition)
        {
            var result = new Result<TEntity>();
            try
            {
                var deletedItem=_dataProvider.Get(condition);
                deletedItem.DeletedDateTime = DateTime.Now;
                deletedItem.isDelete = true;
                _dataProvider.Delete(deletedItem);
                result.Status = Enums.BLLResultType.Success;
                //MagicStringler kaldırılacak.
                result.Message = "Kayıt silme işlemi başarılı bir şekilde gerçekleşti.";
            }
            catch (Exception ex)
            {
                //TODO : Loglama Yapılacak
                result.Status = Enums.BLLResultType.Error;
                //MagicStringler kaldırılacak.
                result.Message = @"Kayıt silme işlemi sırasında bir hata meydana geldi.
                                   Lütfen bu işlemi saati ile birlikte not alıp bildiriniz.";

            }
            return result;
        }
        public virtual Result<TEntity> Get(Expression<Func<TEntity, bool>> condition = null)
        {
            var result = new Result<TEntity>();
            try
            {
               result.Entity= _dataProvider.Get(condition);
                if (result.Entity!=null)
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
        public virtual Result<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> condition = null)
        {
            var result = new Result<List<TEntity>>();
            try
            {
                result.Entity = _dataProvider.GetAll(condition);
          
                 if (result.Entity.Count>0)
                {
                    result.Status = Enums.BLLResultType.Success;
                    //MagicStringler kaldırılacak.
                    result.Message = " Eşleşen "+result.Entity.Count +"  kayıt görüntüleniyor.";
                }
                else if (result.Entity.Count==0)
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
    }
}
