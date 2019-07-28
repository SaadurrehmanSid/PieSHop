using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        [Required]
        public string Address  { get; set; }
        [Required]
        [MaxLength(20)]
        public string Country { get; set; }
    }
}
