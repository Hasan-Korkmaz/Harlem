using System;
using System.Collections.Generic;
using static Harlem.Core.Tools.Enums;

namespace Harlem.Entity.DbModels
{
    public class Basket : BaseEntity
    {
        public string SessionId { get; set; }

        public Guid? AccountUserId { get; set; }

        public PaymentType PaymentType { get; set; }

        public decimal TotalPrice { get; set; }

        public bool isCompleted { get; set; }

        public AccountUserAddress Address { get; set; }
        public AccountUser User { get; set; }

        public ICollection<BasketItem> BasketItem { get; set; }
    }
}
