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
    public class ApiMark : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public ApiMark(ServerProjectContext context)
        {
            _context = context;
        }

        //
        // GET: api/ApiMark
        [HttpGet]
        public IEnumerable<Marks> GetMarks(string rollNumber, int courseId)
        {
            return _context.Marks.Where(m=>m.RollNumber == rollNumber && m.CourseId == courseId);
        }

        // GET: api/ApiMark/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMarks([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marks = await _context.Marks.FindAsync(id);

            if (marks == null)
            {
                return NotFound();
            }

            return Ok(marks);
        }

        // PUT: api/ApiMark/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarks([FromRoute] long id, [FromBody] Marks marks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marks.Id)
            {
                return BadRequest();
            }

            _context.Entry(marks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarksExists(id))
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

        // POST: api/ApiMark
        [HttpPost]
        public async Task<IActionResult> PostMarks([FromBody] Marks marks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Marks.Add(marks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarks", new { id = marks.Id }, marks);
        }

        // DELETE: api/ApiMark/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarks([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marks = await _context.Marks.FindAsync(id);
            if (marks == null)
            {
                return NotFound();
            }

            _context.Marks.Remove(marks);
            await _context.SaveChangesAsync();

            return Ok(marks);
        }

        private bool MarksExists(long id)
        {
            return _context.Marks.Any(e => e.Id == id);
        }
    }
}