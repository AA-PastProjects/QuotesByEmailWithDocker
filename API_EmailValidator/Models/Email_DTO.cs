using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EmailValidator.Models
{
    public class Email_DTO
    {
        public Email_DTO(string a, bool v)
        {
            address = a;
            valid = v;
        }

        public Email_DTO(Email a)
        {
            address = a.email;
            valid = a.valid;
        }

        public string address = "notSet";
        public bool valid = false;
    }
}
