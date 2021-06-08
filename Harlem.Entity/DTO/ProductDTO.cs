using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.Entity.DTO
{
    public class ProductDTO
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public int StockPiece { get; set; }
        public string StockType { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string ProductDetail { get; set; }
        public bool isActive { get; set; }
    }
}
