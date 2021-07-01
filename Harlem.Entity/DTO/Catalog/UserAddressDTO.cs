using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harlem.Entity.DTO.Catalog
{
    public class UserAddressDTO
    {
        [Required]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AddressDetail { get; set; }
    }
}
