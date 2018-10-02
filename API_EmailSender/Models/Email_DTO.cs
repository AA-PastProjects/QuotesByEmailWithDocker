using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EmailSender.Models
{
    public class Email_DTO
    {
        public Email_DTO(string a, string n)
        {
            address = a;
            nameOfReceiver = n;
        }

        /// <summary>
        /// The email address.
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Name of email recipient.
        /// </summary>
        public string nameOfReceiver { get; set; }

    }
    public class EmailWasValid_DTO{

        public string address { get; set; }
        public bool valid { get; set; }
    }
}
