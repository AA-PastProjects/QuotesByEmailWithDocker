using API_EmailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_EmailSender.BLL
{
    public class WikipediaFetcher_Gateway
    {
        public static WikiPage_DTO GetWikipediaPage(string authorName)
        {
            string json;
            json = GetProductAsync("http://api_wikipediafetcher/api/WikiPage/" + authorName).Result;//
            WikiPage_DTO wikiPage = Newtonsoft.Json.JsonConvert.DeserializeObject<WikiPage_DTO>(json);

            if (wikiPage.WikiText.Trim().Equals(""))
            {
                wikiPage.WikiText = "No wikipage found for this author";
            }

            return wikiPage;
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
                product = "Couldn't connect to wikipedia server";
            }

            return product;
        }
    }
}
