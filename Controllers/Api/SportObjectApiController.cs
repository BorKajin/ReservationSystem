using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;
using ReservationSystem.Filters;

namespace ReservationSystem.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportObjectApiController : ControllerBase
    {
        private readonly ReservationContext _context;

        public SportObjectApiController(ReservationContext context)
        {
            _context = context;
        }

        // GET: api/SportObjectApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportObject>>> GetSportObjects()
        {
            return await _context.SportObjects.ToListAsync();
        }

        // GET: api/SportObjectApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SportObject>> GetSportObject(int id)
        {
            var sportObject = await _context.SportObjects.FindAsync(id);

            if (sportObject == null)
            {
                return NotFound();
            }

            return sportObject;
        }

        // PUT: api/SportObjectApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ApiKeyAuth]
        public async Task<IActionResult> PutSportObject(int id, SportObject sportObject)
        {
            if (id != sportObject.ID)
            {
                return BadRequest();
            }

            _context.Entry(sportObject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportObjectExists(id))
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

        // POST: api/SportObjectApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ApiKeyAuth]
        public async Task<ActionResult<SportObject>> PostSportObject(SportObject sportObject)
        {
            _context.SportObjects.Add(sportObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSportObject", new { id = sportObject.ID }, sportObject);
        }

        // DELETE: api/SportObjectApi/5
        [HttpDelete("{id}")]
        [ApiKeyAuth]
        public async Task<IActionResult> DeleteSportObject(int id)
        {
            var sportObject = await _context.SportObjects.FindAsync(id);
            if (sportObject == null)
            {
                return NotFound();
            }

            _context.SportObjects.Remove(sportObject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SportObjectExists(int id)
        {
            return _context.SportObjects.Any(e => e.ID == id);
        }
    }
}
