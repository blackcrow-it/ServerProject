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
        public IEnumerable<Grades> GetClass(string rollNumber)
        {
            Dictionary<int, int> grades = new Dictionary<int, int>();
            var studentGrades = _context.StudentGrade.Where(r => r.RollNumber == rollNumber);
            var j = 0;
            foreach (var item in studentGrades)
            {
                j++;
                grades.Add(j, item.GradeId);
            }
            var too = grades.Values.Distinct().ToArray();
            var listGrades = _context.Grades.Where(g => too.Contains(g.Id));
            return listGrades;
        }
    }
}