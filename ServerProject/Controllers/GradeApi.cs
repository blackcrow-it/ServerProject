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
    public class GradeApi : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public GradeApi(ServerProjectContext context)
        {
            _context = context;
        }

// thong tin lop
        // GET: api/GradeApi
        [HttpGet]
        public IEnumerable<Grades> GetGrades(string nameGrade)
        {
            return _context.Grades.Where(a=> a.Name == nameGrade);
        }
    }
}