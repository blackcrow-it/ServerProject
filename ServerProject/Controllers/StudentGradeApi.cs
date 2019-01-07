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
    public class StudentGradeApi : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public StudentGradeApi(ServerProjectContext context)
        {
            _context = context;
        }

// danh sách học sinh học cùng lớp
        // GET: api/StudentGradeApi
        [HttpGet("list-student")]
        public IEnumerable<StudentGrade> GetStudentGrade(int gradeId)
        {
            return _context.StudentGrade.Where(g=> g.GradeId == gradeId);
        }

// danh sách lớp mà 1 học sinh đang học 
// GET: api/StudentGradeApi/list-class
        [HttpGet("list-class")]
        public IEnumerable<StudentGrade> GetClass(string rollNumber)
        {
            return _context.StudentGrade.Where(i => i.RollNumber == rollNumber);
        }
    }
}