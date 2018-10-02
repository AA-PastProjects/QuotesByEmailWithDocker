using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EmailValidator.Models
{
    public class Email
    {
        public Email(string e) {
            email = e;
        }
        public string email = "notSet";
        public bool valid = false;
    }
}
