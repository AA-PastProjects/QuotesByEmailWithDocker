using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_WikipediaFetcher.Models
{
    public class WikiPage_DTO
    {
        public WikiPage_DTO(string wt) {
            WikiText = wt;
        }

        public WikiPage_DTO(WikiPage wp)
        {
            WikiText = wp.WikiText;
        }

        public string WikiText { get; set; }
    }
}
