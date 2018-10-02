using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_EmailSender.Models
{
    public class WikiPage_DTO
    {
        public WikiPage_DTO(string wt) {
            WikiText = wt;
        }

        public string WikiText { get; set; }
    }
}
