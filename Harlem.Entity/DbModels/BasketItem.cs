using System;

namespace Harlem.Entity.DbModels
{
    public class BasketItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid BasketId { get; set; }


        public int Qty { get; set; }
        public decimal Price { get; set; }

        public Basket Basket { get; set; }
        public Product Product { get; set; }
    }
}
