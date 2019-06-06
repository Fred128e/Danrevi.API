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
    public class AttendMødeController:ControllerBase
    {
        private readonly DanreviDbContext _context;

        public AttendMødeController(DanreviDbContext context)
        {
            _context = context;
        }

        // GET: api/AttendMøde
        [HttpGet]
        public IEnumerable<MøderBruger> GetMøderBruger()
        {
            return _context.MøderBruger;
        }

        // GET: api/AttendMøde/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMøderBruger([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var møderBruger = await _context.MøderBruger.FindAsync(id);

            if(møderBruger == null)
            {
                return NotFound();
            }

            return Ok(møderBruger);
        }

        // PUT: api/AttendMøde/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMøderBruger([FromRoute] int id,[FromBody] MøderBruger møderBruger)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != møderBruger.MødeId)
            {
                return BadRequest();
            }

            _context.Entry(møderBruger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!MøderBrugerExists(id))
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

        // POST: api/AttendMøde
        [HttpPost("{id}/{mId}")]
        public async Task<IActionResult> PostMøderBruger(string id,int mId)
        {
            var bruger = _context.Brugere.Find(id);
            var møder = _context.Møder.Find(mId);
            MøderBruger m = new MøderBruger
            {
                MødeId = mId,
                Uid = id.ToString(),
                Møde = møder,
                U = bruger
            };

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MøderBruger.Add(m);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                if(MøderBrugerExists(m.MødeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/AttendMøde/5
        [HttpDelete("{id}/{mId}")]
        public async Task<IActionResult> DeleteMøderBruger([FromRoute] string id,int mId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _context.Brugere.Find(id);
                if(user == null)
                    return NotFound();

                var møder = _context.Møder.Find(mId);
                MøderBruger m = new MøderBruger
                {
                    MødeId = mId,
                    Uid = id.ToString(),
                    Møde = møder,
                    U = user
                };

                _context.MøderBruger.Remove(m);
                await _context.SaveChangesAsync();

                return Ok(m);
            }
            catch(Exception)
            {
                return NotFound();
            }

        }

        private bool MøderBrugerExists(int id)
        {
            return _context.MøderBruger.Any(e => e.MødeId == id);
        }
    }
}