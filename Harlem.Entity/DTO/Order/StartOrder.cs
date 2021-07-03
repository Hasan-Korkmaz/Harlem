using Harlem.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harlem.Entity.DTO.Order
{
   public  class StartOrder
    {
        public Guid BasketId{ get; set; }
        public Guid AddressId{ get; set; }
        public Enums.PaymentType PaymentType{ get; set; }
    }
}
