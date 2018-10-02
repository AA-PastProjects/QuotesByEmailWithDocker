using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API_QuoteFetcher.Controllers
{
    public class QuoteFetcher
    {//http://json2csharp.com/

        public static Models.Quote GetQuote()
        {
            string json;
            Models.Quote q;
            json = GetProductAsync("https://talaikis.com/api/quotes/random/").Result;
            if (SeemsLikeValidReply(json))
            {
                q = new Models.Quote(json);
            }
            else
            {
                q = new Models.Quote();
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
}
