using Harlem.Core.Tools;
using System;
using System.Collections.Generic;

namespace Harlem.Entity.DTO.Catalog
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StockPiece { get; set; }
        public Enums.StockType StockType { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string ProductDetail { get; set; }
        public bool isActive { get; set; }
        public List<ProductImageDTO> ProductImages {get;set;}
    }
}
