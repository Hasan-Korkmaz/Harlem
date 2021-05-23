using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.DAL.Concrete.DataAccesLayers
{
  public  class ProductDAL:DataAccessTemplate<Context.HarlemContext,Product>, IProductDAL
    {
    }
}
