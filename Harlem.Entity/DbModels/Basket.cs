using System;
using System.Collections.Generic;
using static Harlem.Core.Tools.Enums;

namespace Harlem.Entity.DbModels
{
    public class Basket : BaseEntity
    {
        public Guid? SessionId { get; set; }
        public Guid? AccountUserId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool isCompleted { get; set; }
        public AccountUser AccountUser { get; set; }
        public ICollection<BasketItem> BasketItem { get; set; }
    }
}
