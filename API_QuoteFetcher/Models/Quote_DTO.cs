using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QuoteFetcher.Models
{
    public class Quote_DTO
    {
        public Quote_DTO(string q, string a) {
            quote = q;
            author = a;
        }

        public Quote_DTO(Quote q)
        {
            quote = q.quote;
            author = q.author;
        }

        public string quote { get; set; }
        public string author { get; set; }
    }
}
