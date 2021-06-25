using Harlem.DAL.Abstract;
using Harlem.DAL.Concrete.DataAccesLayers.Template;
using Harlem.Entity.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.DAL.Concrete.DataAccesLayers
{
    public class ProductImageDAL : DataAccessTemplate<Context.HarlemContext, ProductImage>, IProductImageDAL
    {

    }
   
}
