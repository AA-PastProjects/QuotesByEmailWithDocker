using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace API_QuoteFetcher.Controllers
{
    [Produces("application/json")]
    [Route("api/Quote")]
    public class QuoteController : Controller
    {
        //GET: api/Quote
       [HttpGet]
        public Models.Quote_DTO Get()
        {
            Models.Quote q = QuoteFetcher.GetQuote();
            var qdto = new Models.Quote_DTO(q);
            return qdto;
        }
    }
}
