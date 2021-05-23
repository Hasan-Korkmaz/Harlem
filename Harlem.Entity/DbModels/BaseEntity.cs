using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.Entity.DbModels
{
   public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool isActive { get; set; } 
        public bool isDelete { get; set; } 
        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
    }
}
