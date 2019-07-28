using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.ViewModels
{
    public class adminRegUser
    {
         [Required]
         [MaxLength(30),Display(Name="User Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(11,ErrorMessage ="Phone number can not exceed 11 digits")]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password Mismatch")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [MaxLength(20)]
        public string Country { get; set; }
    }
}
