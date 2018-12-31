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
    public class InformationsController : ControllerBase
    {
        private readonly ServerProjectContext _context;

        public InformationsController(ServerProjectContext context)
        {
            _context = context;
        }

        // GET: api/Informations
        [HttpGet]
        public IEnumerable<Informations> GetInformations()
        {
            return _context.Informations;
        }

        // GET: api/Informations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInformations([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var informations = await _context.Informations.FindAsync(id);

            if (informations == null)
            {
                return NotFound();
            }

            return Ok(informations);
        }

        // PUT: api/Informations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformations([FromRoute] int id, [FromBody] Informations informations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != informations.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(informations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformationsExists(id))
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

        // POST: api/Informations
        [HttpPost]
        public async Task<IActionResult> PostInformations([FromBody] Informations informations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Informations.Add(informations);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InformationsExists(informations.AccountId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInformations", new { id = informations.AccountId }, informations);
        }

        // DELETE: api/Informations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformations([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var informations = await _context.Informations.FindAsync(id);
            if (informations == null)
            {
                return NotFound();
            }

            _context.Informations.Remove(informations);
            await _context.SaveChangesAsync();

            return Ok(informations);
        }

        private bool InformationsExists(int id)
        {
            return _context.Informations.Any(e => e.AccountId == id);
        }
    }
}