using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_QuoteFetcher.Models
{
    public class Quote
    {
        public Quote(string json) //Source on converting JSON -> C# object: https://stackoverflow.com/questions/2246694/how-to-convert-json-object-to-custom-c-sharp-object
        {
            JObject jObject = JObject.Parse(json);
            quote = (string)jObject["quote"];
            author = (string)jObject["author"];
            cat = (string)jObject["cat"];
        }

        public Quote() { }

        public string quote { get; set; }
        public string author { get; set; }
        public string cat { get; set; }

    }
}
