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
                    var Information = new AdditionRollnumber();
                    Information.informations = existInformations;
                    Information.RollNumber = _context.Students.SingleOrDefault(i => i.AccountId == existToken.OwnerId).RollNumber;
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return new JsonResult(Information);
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
                    var password = Handlepassword.GetInstance().EncryptPassword(changePassword.newPassword, existAccount.Salt);  
                    if(existAccount.Password == Handlepassword.GetInstance().EncryptPassword(changePassword.Password, existAccount.Salt))
                    {
                        existAccount.Password = password;
                        _context.Accounts.Update(existAccount);
                        _context.SaveChanges();
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return new JsonResult(existAccount);
                    }
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return new JsonResult("Password not found");
                }

                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return new JsonResult("Not Found");
            }
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return new JsonResult("Forbidden");
        }

// danh sách môn học
        //GET: api/HandleApi/list-courses
        [HttpGet("list-courses")]
        public async Task<IActionResult> ListCourse(string rollNumber)
        {
            Dictionary<int, int> dicGrade = new Dictionary<int, int>();
            Dictionary<int, int> courses = new Dictionary<int, int>();
            var studentGrades = _context.StudentGrade.Where(r => r.RollNumber == rollNumber);
            var j = 0;
            foreach (var item in studentGrades)
            {
                j++;
                dicGrade.Add(j, item.GradeId);
            }
            var too = dicGrade.Values.ToArray();
            var grades = _context.GradeCourse.Where(g => too.Contains(g.GradeId));
            var i = 0;
            foreach (var item in grades)
            {
                i++;
                courses.Add(i, item.GradeId);
            }
            var foo = courses.Values.ToArray();
            var listCourses = _context.Courses.Where(a => foo.Contains(a.Id));
            return new JsonResult(listCourses);
        }

// danh sách học sinh trong lớp
        //GET : api/HandeApi/list-students
        //[HttpGet("list-students")]
        //public async Task<IActionResult> ListStudent(string nameGrade)
        //{
        //    Dictionary<string, string> Students = new Dictionary<string, string>();
        //    var students = _context.Grades.Where(a => a.Name == nameGrade);
        //    var s = 0;
        //    foreach (var item in students)
        //    {
        //        s++;
        //        Students.Add(s, item.Name);
        //    }
        //    var an = Students.Values.ToArray();
        //    var listStudent = _context.Students.Where(a => an.Contains(a.RollNumber));
        //    return new JsonResult(listStudent);
        //}
    }
}