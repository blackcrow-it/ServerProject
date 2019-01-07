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
    public class ApiCourse : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public ApiCourse(ServerProjectContext context)
        {
            _context = context;
        }
        [HttpGet("list-courses")]
        public IEnumerable<Courses> ListCourse(string rollNumber)
        {
            Dictionary<int, int> dicGrade = new Dictionary<int, int>();
            Dictionary<int, int> courses = new Dictionary<int, int>();
            var grades = _context.StudentGrade.Where(r => r.RollNumber == rollNumber);
            var i = 0;
            foreach (var item in grades)
            {
                i++;
                dicGrade.Add(i, item.GradeId);
            }
            var foo = dicGrade.Values.ToArray();
            var listGradeCourses = _context.GradeCourse.Where(a => foo.Contains(a.GradeId));
            i = 0;
            foreach (var item in listGradeCourses)
            {
                i++;
                courses.Add(i, item.CourseId);
            }
            var too = courses.Values.ToArray();
            var listCourses = _context.Courses.Where(a => too.Contains(a.Id));
            return listCourses;
        }
        // GET: api/ApiCourse
        [HttpGet]
        public IEnumerable<Courses> GetCourses()
        {
            return _context.Courses;
        }

        // GET: api/ApiCourse/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courses = await _context.Courses.FindAsync(id);

            if (courses == null)
            {
                return NotFound();
            }

            return Ok(courses);
        }

        // PUT: api/ApiCourse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourses([FromRoute] int id, [FromBody] Courses courses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courses.Id)
            {
                return BadRequest();
            }

            _context.Entry(courses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiCourse
        [HttpPost]
        public async Task<IActionResult> PostCourses([FromBody] Courses courses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Courses.Add(courses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourses", new { id = courses.Id }, courses);
        }

        // DELETE: api/ApiCourse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(courses);
            await _context.SaveChangesAsync();

            return Ok(courses);
        }

        private bool CoursesExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}