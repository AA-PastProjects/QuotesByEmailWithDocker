using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_WikipediaFetcher.Models
{
    public class WikiPage
    {
        public WikiPage(string json)
        {
            bool isNextLine = false;
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            int line = 0;
            while (reader.Read())
            {
                if (isNextLine)
                {
                    WikiText = reader.Value.ToString();
                    Console.WriteLine("newly found wikitext: " + WikiText);
                    break;
                }

                if (reader.Value != null && reader.Value.Equals("extract"))
                {
                    isNextLine = true;
                }
            }

        }
        public string WikiText { get; set; }
    }

}
