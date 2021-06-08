using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.Entity.DbModels
{
   public class Category:BaseEntity
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
