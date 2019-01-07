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
    public class GradeApi : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public GradeApi(ServerProjectContext context)
        {
            _context = context;
        }

        //// thong tin lop
        //// GET: api/GradeApi
        [HttpGet("list-student")]
        public IActionResult GetStudents(int gradeId)
        {
            Dictionary<int, int> listStudents = new Dictionary<int, int>();
            var students = _context.StudentGrade.Where(a => a.GradeId == gradeId);
            var s = 0;
            foreach (var item in students)
            {
                s++;
                var studentH = _context.Students.FirstOrDefault(i => i.RollNumber == item.RollNumber);
                listStudents.Add(s, studentH.AccountId);
            }
            var an = listStudents.Values.ToArray();
            var listStudent = _context.Informations.Where(a => an.Contains(a.AccountId));
            return new JsonResult(listStudent);
        }
        
    }
}