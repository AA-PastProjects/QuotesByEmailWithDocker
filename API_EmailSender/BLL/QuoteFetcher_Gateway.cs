using API_EmailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_EmailSender.BLL
{
    public class QuoteFetcher_Gateway
    {
        public static Quote_DTO GetQuote()
        {
            string json;
            json = GetProductAsync("http://api_quotefetcher/api/Quote/").Result;//
            Quote_DTO q = Newtonsoft.Json.JsonConvert.DeserializeObject<Quote_DTO>(json);
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                product = "Couldn't connect to quotes server";
            }

            return product;
        }
    }
}
