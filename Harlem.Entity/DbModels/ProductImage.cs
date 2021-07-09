using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Harlem.Entity.DbModels
{
    public class ProductImage:BaseEntity
    {
        public string ImageName { get; set; }
        public string PublicURL { get; set; }
        public string PhysicalPath { get; set; }
        public string PhysicalName { get; set; }
        public string ImageAltValue { get; set; }
        public bool isMainImage { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public int Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
