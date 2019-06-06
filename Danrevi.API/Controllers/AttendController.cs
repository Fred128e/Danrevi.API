using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Danrevi.API.Models;
using Microsoft.CodeAnalysis.Options;

namespace Danrevi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendController : ControllerBase
    {
        private readonly DanreviDbContext _context;

        public AttendController(DanreviDbContext context)
        {
            _context = context;
        }

        // GET: api/Attend
        [HttpGet]
        public IEnumerable<BrugerKurser> GetBrugerKurser()
        {
            return _context.BrugerKurser;
        }

        // GET: api/Attend/5
        [HttpGet("{id}")]
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

        // PUT: api/Attend/5
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

        // POST: api/Attend
        [HttpPost("{id}/{kid}")]
        public async Task<IActionResult> PostBrugerKurser(string id, int kid)


//        public async Task<IActionResult> PostBrugerKurser([FromBody] BrugerKurser brugerKurser)
        {

            var user = _context.Brugere.Find(id);
            var kursus = _context.Kurser.Find(kid);
            BrugerKurser k  = new BrugerKurser();
            k.KursusId = kid;
            k.Uid = id.ToString();
            k.Kursus = kursus;
            k.U = user;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BrugerKurser.Add(k);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BrugerKurserExists(k.KursusId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return Ok();
           // return CreatedAtAction("GetBrugerKurser", new { id = k.KursusId }, k);
        }

        // DELETE: api/Attend/5
        [HttpDelete("{id}/{kid}")]
        public async Task<IActionResult> DeleteBrugerKurser(string id, int kid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var brugerKurser = await _context.BrugerKurser.FindAsync(id);
            //if(brugerKurser == null)
            //{
            //    return NotFound();
            //}

            
            try
            {
                var user = _context.Brugere.Find(id);
                if(user == null)
                    return NotFound();

                var kursus = _context.Kurser.Find(kid);
                if(kursus == null)
                    return NotFound();
                BrugerKurser k = new BrugerKurser();
                k.KursusId = kid;
                k.Uid = id.ToString();
                k.Kursus = kursus;
                k.U = user;
                _context.BrugerKurser.Remove(k);
                await _context.SaveChangesAsync();

                return Ok(k);

            }
            catch (Exception)
            {
                return NotFound();
            }
            
        }

        private bool BrugerKurserExists(int id)
        {
            return _context.BrugerKurser.Any(e => e.KursusId == id);
        }
    }
}