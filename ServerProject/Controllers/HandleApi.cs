using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerProject.Models;

namespace ServerProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleApi : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public HandleApi(ServerProjectContext context)
        {
            _context = context;
        }

// get student's information
        //GET: api/HandleApi/information-student
        [HttpGet("information-student")]
        public async Task<IActionResult> InformationStudent()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var basicToken = Request.Headers["Authorization"].ToString();
            var token = basicToken.Replace("Basic ", "");
            var existToken = _context.Credentials.SingleOrDefault(c => c.AccessToken == token);
            if(existToken != null)
            {
                var Id = existToken.OwnerId;
                var existInformations = _context.Informations.SingleOrDefault(i => i.AccountId == existToken.OwnerId);
                if (existInformations != null)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return new JsonResult(existInformations);
                }
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new JsonResult("Forbidden");
            }
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return new JsonResult("Not Found");
        }

// update student's information
        [HttpPost("change-information")]
        public async Task<IActionResult> ChangeStudentInformation(ChangeStudentInformation changeStudentInformation)
        {

            if (!ModelState.IsValid)
            {
                return new JsonResult("BadRequest");
            }
            var basicToken = Request.Headers["Authorization"].ToString();
            var token = basicToken.Replace("Basic ", "");
            var existToken = _context.Credentials.SingleOrDefault(a => a.AccessToken == token);
            if (existToken != null)
            {
                var existInformations = _context.Informations.SingleOrDefault(i => i.AccountId == existToken.OwnerId);
                    if (existInformations != null)
                    {
                        var email = changeStudentInformation.newEmail;
                         existInformations.Email = email;

                         var phone = changeStudentInformation.newPhone;
                         existInformations.Phone = phone;

                        var address = changeStudentInformation.newAddress;
                        existInformations.Address = address;

                        var avatar = changeStudentInformation.newAvatar;
                        existInformations.Avatar = avatar;


                    _context.Informations.Update(existInformations);
                    _context.SaveChanges();
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return new JsonResult(existInformations);

                    }
            }
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return new JsonResult("Forbidden");
        }
// update student's password
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {

            if (!ModelState.IsValid)
            {
                return new JsonResult("BadRequest");
            }
            var basicToken = Request.Headers["Authorization"].ToString();
            var token = basicToken.Replace("Basic ", "");
            var existToken = _context.Credentials.SingleOrDefault(a => a.AccessToken == token);
            if (existToken != null)
            {
                var existAccount = _context.Accounts.SingleOrDefault(i => i.Id == existToken.OwnerId);
                if (existAccount != null)
                {
                    var password = changePassword.newPassword;
                    existAccount.Password = password;
                    _context.Accounts.Update(existAccount);
                    _context.SaveChanges();
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return new JsonResult(existAccount);

                }
            }
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return new JsonResult("Forbidden");
        }
// List of classes that students are attending
        [HttpGet("list-class")]
        public async Task<IActionResult> ListClass()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("BadRequest");
            }
            var basicToken = Request.Headers["Authorization"].ToString();
            var token = basicToken.Replace("Basic ", "");
            var existToken = _context.Credentials.SingleOrDefault(a => a.AccessToken == token);
            var existGrade = _context.StudentGrade.ToList();
            return new JsonResult(existGrade);
        }
    }
}