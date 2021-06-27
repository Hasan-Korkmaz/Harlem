using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harlem.Entity.DTO.Catalog
{
    public class UserAddressDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string AddressDetail { get; set; }
    }
}
