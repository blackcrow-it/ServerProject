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
    public class ApiGradeCourse : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public ApiGradeCourse(ServerProjectContext context)
        {
            _context = context;
        }
//danh sach mon cua 1 lop
        // GET: api/ApiGradeCourse
        [HttpGet]
        public IEnumerable<GradeCourse> GetGradeCourse(int gradeId)
        {
            return _context.GradeCourse.Where(a=> a.GradeId == gradeId);
        }
    }
}