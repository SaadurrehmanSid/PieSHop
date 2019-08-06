using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Utilities
{
    public class EmailValidationCustom : ValidationAttribute
    {

        public EmailValidationCustom(string email)
        {
            _email = email;
        }

        public string _email { get; }

        public override bool IsValid(object value)
        {

            string[] str = value.ToString().Split('@');
            return str[1].ToUpper() == _email.ToUpper();
        }
    }
}
