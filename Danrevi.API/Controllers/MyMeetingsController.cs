using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Danrevi.API.Models;

namespace Danrevi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyMeetingsController : ControllerBase
    {
        private readonly DanreviDbContext _context;

        public MyMeetingsController(DanreviDbContext context)
        {
            _context = context;
        }

        // GET: api/MyMeetings
        [HttpGet]
        public IEnumerable<MøderBruger> GetMøderBruger()
        {
            var res = _context.MøderBruger.Include(m => m.Møde);
            return res;
        }

        // GET: api/MyMeetings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMøderBruger([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var møderBruger = _context.MøderBruger.Where(m => m.Uid.CompareTo(id) == 0).Select(m => m.Møde);

            if (møderBruger == null)
            {
                return NotFound();
            }

            return Ok(møderBruger);
        }

        // PUT: api/MyMeetings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMøderBruger([FromRoute] int id, [FromBody] MøderBruger møderBruger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != møderBruger.MødeId)
            {
                return BadRequest();
            }

            _context.Entry(møderBruger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MøderBrugerExists(id))
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

        // POST: api/MyMeetings
        [HttpPost]
        public async Task<IActionResult> PostMøderBruger([FromBody] MøderBruger møderBruger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MøderBruger.Add(møderBruger);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MøderBrugerExists(møderBruger.MødeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMøderBruger", new { id = møderBruger.MødeId }, møderBruger);
        }

        // DELETE: api/MyMeetings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMøderBruger([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var møderBruger = await _context.MøderBruger.FindAsync(id);
            if (møderBruger == null)
            {
                return NotFound();
            }

            _context.MøderBruger.Remove(møderBruger);
            await _context.SaveChangesAsync();

            return Ok(møderBruger);
        }

        private bool MøderBrugerExists(int id)
        {
            return _context.MøderBruger.Any(e => e.MødeId == id);
        }
    }
}