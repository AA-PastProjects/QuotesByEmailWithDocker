using API_EmailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_EmailSender.BLL
{
    public class EmailValidator_Gateway
    {
        public static EmailWasValid_DTO ValidateEmail(string emailAddress)
        {
            string json;
            json = GetProductAsync("http://api_emailvalidator/api/EmailValidation/"+emailAddress).Result;//
            EmailWasValid_DTO ewvdto = Newtonsoft.Json.JsonConvert.DeserializeObject<EmailWasValid_DTO>(json);
            return ewvdto;
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
                product = "Couldn't connect to email validator";
            }

            return product;
        }
    }
}
