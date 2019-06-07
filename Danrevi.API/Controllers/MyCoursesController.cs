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
    public class MyCoursesController : ControllerBase
    {
        private readonly DanreviDbContext _context;

        public MyCoursesController(DanreviDbContext context)
        {
            _context = context;
        }

        // GET: api/MyCourses
        [HttpGet]
        public IEnumerable<BrugerKurser> GetBrugerKurser()
        {
            var res = _context.BrugerKurser.Include(c => c.Kursus);
                
                

            return res;
            // 
        }

        // GET: api/MyCourses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMyCurses([FromRoute] string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brugerKurser = _context.BrugerKurser.Where(x => x.Uid.CompareTo(id) == 0).Select(x=>x.Kursus);
            if(brugerKurser == null)
            {
                return NotFound();
            }

            return Ok(brugerKurser);
        }
        // GET: api/MyCourses/5
        [HttpGet("{id}/{uid}")]
        public async Task<IActionResult> GetBrugerKurser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brugerKurser = await _context.BrugerKurser.FindAsync(id);

            if (brugerKurser == null)
            {
                return NotFound();
            }

            return Ok(brugerKurser);
        }

        // PUT: api/MyCourses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrugerKurser([FromRoute] int id, [FromBody] BrugerKurser brugerKurser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brugerKurser.KursusId)
            {
                return BadRequest();
            }

            _context.Entry(brugerKurser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrugerKurserExists(id))
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

        // POST: api/MyCourses
        [HttpPost]
        public async Task<IActionResult> PostBrugerKurser([FromBody] BrugerKurser brugerKurser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BrugerKurser.Add(brugerKurser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BrugerKurserExists(brugerKurser.KursusId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBrugerKurser", new { id = brugerKurser.KursusId }, brugerKurser);
        }

        // DELETE: api/MyCourses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrugerKurser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brugerKurser = await _context.BrugerKurser.FindAsync(id);
            if (brugerKurser == null)
            {
                return NotFound();
            }

            _context.BrugerKurser.Remove(brugerKurser);
            await _context.SaveChangesAsync();

            return Ok(brugerKurser);
        }

        private bool BrugerKurserExists(int id)
        {
            return _context.BrugerKurser.Any(e => e.KursusId == id);
        }
    }
}