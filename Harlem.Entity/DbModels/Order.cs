using System;
using System.Collections.Generic;
using static Harlem.Core.Tools.Enums;

namespace Harlem.Entity.DbModels
{
    public class Order : BaseEntity
    {
        public Guid AddressId { get; set; }
        public Guid AccountUserId { get; set; }
        
        public PaymentType PaymentType { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        public decimal TotalPrice { get; set; }

        public bool isDelivered { get; set; }

        public AccountUserAddress Address { get; set; }
        public AccountUser User { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
