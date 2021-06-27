using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harlem.Entity.FrontEndTypes.ViewModels
{
    public class AccountUserRegisterViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(25)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
