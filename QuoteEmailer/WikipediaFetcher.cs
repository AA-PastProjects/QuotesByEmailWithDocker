using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace QuoteEmailer
{
    public class WikipediaFetcher
    {
        public WikiPage GetWikipediaPage(string authorName) {
            string authorUrlString = urlifyAuthorName(authorName);
            //wiki URL search is case sensitive!
            string json = GetProductAsync("https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro=&explaintext=&titles="+ authorUrlString).Result; //https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro=&explaintext=&titles=Stack%20Overflow
            WikiPage wikiPage = new WikiPage(json);
            return wikiPage;
        }

        string urlifyAuthorName(string authorName) {
            var authorNameAsChars = new List<char>();
            foreach (char c in authorName)
            {
                if (c == ' ')
                {
                    //%20
                    authorNameAsChars.Add('%');
                    authorNameAsChars.Add('2');
                    authorNameAsChars.Add('0');
                }
                else
                {
                    authorNameAsChars.Add(c);
                }
            }
            StringBuilder builder = new StringBuilder(authorNameAsChars.Count);
            foreach (var cha in authorNameAsChars)
            {
                builder.Append(cha);
            }
            string urlified = builder.ToString();
            Console.WriteLine("urlified: "+ urlified);
            return urlified;
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
                product = "Couldn't connect to wiki server";
            }
            //Console.WriteLine("Wikipedia fetch: "+ product);
            return product;
        }

        static bool SeemsLikeValidReply(string reply)
        {

            if (reply.Equals("Couldn't connect to wiki server") || reply.Trim().Equals(""))
            {
                return false;
            }
            return true;
        }
    }

    public class WikiPage
    {
        public WikiPage(string json) {
            bool isNextLine = false;
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            int line = 0;
            while (reader.Read())
            {
                //Console.WriteLine("line: " + line);
                //line++;
                //Console.WriteLine(reader.TokenType);
                //Console.WriteLine(reader.Value);
                if (isNextLine)
                {
                    wikiText = reader.Value.ToString();
                    Console.WriteLine("newly found wikitext: "+wikiText);
                    break;
                }

                if (reader.Value != null && reader.Value.Equals("extract"))
                {
                    isNextLine = true;
                }
            }

        }
        public string wikiText;
    }
}
