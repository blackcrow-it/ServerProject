using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerProject.Models;

namespace ServerProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SstudentGradeApi : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public SstudentGradeApi(ServerProjectContext context)
        {
            _context = context;
        }

// danh sách học sinh học cùng lớp
        // GET: api/SstudentGradeApi
        [HttpGet("list-student")]
        public IEnumerable<StudentGrade> GetStudentGrade(int gradeId)
        {
            return _context.StudentGrade.Where(c=> c.GradeId == gradeId);
        }

// danh sách lớp mà 1 học sinh đang học 
// GET: api/SstudentGradeApi/list-class
        [HttpGet("list-class")]
        public IEnumerable<StudentGrade> GetClass(string rollNumber)
        {
            return _context.StudentGrade.Where(d => d.RollNumber == rollNumber);
        }
    }
}