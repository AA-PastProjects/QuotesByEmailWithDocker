using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EmailValidator.Controllers
{
    [Produces("application/json")]
    [Route("api/EmailValidation")]
    public class EmailValidationController : Controller
    {
        // GET: api/EmailValidation/5
        [HttpGet("{email}", Name = "Get")]
        public Models.Email_DTO Get(string email)
        {
            //make internal email object.
            Models.Email emailObject = new Models.Email(email);
            //Check email
            emailObject.valid = EmailValidator.IsValidEmail(emailObject.email);
            //store in DTO and return.
            Models.Email_DTO edto = new Models.Email_DTO(emailObject);
            return edto;
        }
    }
}
