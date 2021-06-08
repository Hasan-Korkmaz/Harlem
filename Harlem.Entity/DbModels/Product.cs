using Harlem.Core.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.Entity.DbModels
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int StockPiece { get; set; }
        public Enums.StockType StockType { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string ProductDetail { get; set; }
        public virtual Category Category { get; set;}
        
    }
}
