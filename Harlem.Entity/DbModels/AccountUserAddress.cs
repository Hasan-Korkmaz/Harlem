using System;
using System.Collections.Generic;

namespace Harlem.Entity.DbModels
{
    public class AccountUserAddress : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }


        public AccountUser User { get; set; }
        public ICollection <Order> Orders { get; set; }
    }

}
