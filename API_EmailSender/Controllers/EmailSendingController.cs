using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_EmailSender.BLL;
using RestSharp;

namespace API_EmailSender.Controllers
{
    [Produces("application/json")]
    [Route("api/EmailSending")]
    public class EmailSendingController : Controller
    {
        // GET: api/EmailSending
        [HttpGet]
        public Models.Email_DTO Get()
        {
            var email = new Models.Email_DTO("Example@example.com", "Firstname Lastname");
            return email;
        }

        // POST: api/EmailSending
        [HttpPost]
        public IActionResult Post([FromBody]Models.Email_DTO email_dto)
        {
            var email = new Models.Email(email_dto);
            bool emailValid = EmailValidator_Gateway.ValidateEmail(email.Address).valid;
            if (emailValid) //send email.
            {
                var q = QuoteFetcher_Gateway.GetQuote();//response.Data;
                var wp = WikipediaFetcher_Gateway.GetWikipediaPage(q.author);
                EmailSender.SendEmailWithSmtp(email, q, wp);
                return Json("Email sent to " + email_dto.address);
            }
            else //give error message.
            {
                return Json("Could not validate email: " + email_dto.address);
            }
        }


    }
}
