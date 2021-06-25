using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Harlem.Entity.DbModels
{
    [Serializable]

    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public bool isActive { get; set; } 
        public bool isDelete { get; set; } 
        public DateTime InsertDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
    }
}
