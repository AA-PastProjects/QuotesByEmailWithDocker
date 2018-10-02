using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QuoteEmailer
{
    public class QuoteFetcher
    {//http://json2csharp.com/

        public Quote GetQuote()
        {
            string json;
            Quote q;
            json = GetProductAsync("https://talaikis.com/api/quotes/random/").Result;
            if (SeemsLikeValidReply(json))
            {
                q = new Quote(json);
            }
            else
            {
                q = new Quote();
                q.quote = json;
                q.author = json;
            }
            return q;
        }

        static HttpClient client = new HttpClient();
        static async Task<string> GetProductAsync(string path)
        {
            string product = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsStringAsync();
                }
            }
            catch(Exception e) {
                product = "Couldn't connect to quotes server";
            }

            return product;
        }

        static bool SeemsLikeValidReply(string reply) {

            if (reply.Equals("Couldn't connect to quotes server") || reply.Trim().Equals(""))
            {
                return false;
            }
            return true;
        }

    }
 
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
