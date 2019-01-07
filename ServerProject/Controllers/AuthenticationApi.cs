using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerProject.Models;
using ServerProject.Security;

namespace ServerProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationApi : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public AuthenticationApi(ServerProjectContext context)
        {
            _context = context;
        }
        // POST: api/AuthenticationApi
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Authentication authentication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existAccount = _context.Accounts.SingleOrDefault(a => a.UserName == authentication.UserName);
            if (existAccount != null)
            {
                if(existAccount.Role == 0)
                {
                    var password = Handlepassword.GetInstance().EncryptPassword(authentication.Password, existAccount.Salt);
                    if(password == existAccount.Password)
                    {
                        var credential = new Credential(existAccount.Id);
                        _context.Add(credential);
                        _context.SaveChanges();
                        return new JsonResult(credential);
                    }
                }
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new JsonResult("Forbidden");
            }
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return new JsonResult("NotFound");
        }        
    }
}