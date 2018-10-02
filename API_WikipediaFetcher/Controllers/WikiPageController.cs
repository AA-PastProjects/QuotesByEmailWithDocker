using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WikipediaFetcher.Controllers
{
    [Produces("application/json")]
    [Route("api/WikiPage")]
    public class WikiPageController : Controller
    {
        // GET: api/WikiPage/5
        /// <summary>
        /// Authorname must have the right capitalization, e.g. 'Henry Ford'
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns></returns>
        [HttpGet("{authorName}", Name = "Get")]
        public Models.WikiPage_DTO Get(string authorName)
        {
            Models.WikiPage wp = WikipediaFetcher.GetWikipediaPage(authorName);
            Models.WikiPage_DTO wpdto = new Models.WikiPage_DTO(wp);
            return wpdto;
        }
    }
}
