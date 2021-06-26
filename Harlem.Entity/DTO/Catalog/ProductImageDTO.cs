using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harlem.Entity.DTO.Catalog
{
    public class ProductImageDTO
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public string PublicURL { get; set; }
        public string PhysicalPath { get; set; }
        public string PhysicalName { get; set; }
        public string ImageAltValue { get; set; }
        public bool isMainImage { get; set; }
        public int Order { get; set; }
        public Guid ProductId { get; set; }
    }
}
