using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.ViewModels
{
    public class EditRoleVM
    {
        public EditRoleVM()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<string> Users { get; set; }
    }
}
