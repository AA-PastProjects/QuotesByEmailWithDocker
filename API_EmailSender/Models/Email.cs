using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EmailSender.Models
{
    public class Email
    {
        public Email(string a, string n)
        {
            Address = a;
            NameOfReceiver = n;
        }

        public Email(string a, string n, bool v)
        {
            Address = a;
            NameOfReceiver = n;
            Valid = v;
        }

        public Email(Email_DTO edto, bool v = false)
        {
            Address = edto.address;
            NameOfReceiver = edto.nameOfReceiver;
            Valid = v;
        }
        /// <summary>
        /// The email address.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Name of email recipient.
        /// </summary>
        public string NameOfReceiver { get; set; }
        /// <summary>
        /// Whether or not the email has been validated.
        /// </summary>
        public bool Valid { get; set; }
    }
}
