using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Danrevi.API.Models;
using Danrevi.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace Danrevi.API.Controllers
{
    [Route("api/møder")]
    [ApiController]
    [Authorize]
    public class MøderController : ControllerBase
    {
        private readonly DanreviDbContext _context;
        private readonly IUserContext _usercontext;

        public MøderController(DanreviDbContext context, IUserContext admin)
        {
            _context = context;
            _usercontext = admin;
        }

        // GET: api/Møder
        [HttpGet]
        public IEnumerable<Møder> GetMøder()
        {
            
            return _context.Møder;
        }

        // GET: api/Møder/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMøder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdmin = _usercontext.IsAdmin(this.User);
            if(!isAdmin)
            {
                return null;
            }

            var møder = await _context.Møder.FindAsync(id);

            if (møder == null)
            {
                return NotFound();
            }

            return Ok(møder);
        }

        // PUT: api/Møder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMøder([FromRoute] int id, [FromBody] Møder møder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdmin = _usercontext.IsAdmin(this.User);
            if(!isAdmin)
            {
                return null;
            }

            if (id != møder.Id)
            {
                return BadRequest();
            }

            _context.Entry(møder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MøderExists(id))
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

        // POST: api/Møder
        [HttpPost]
        public async Task<IActionResult> PostMøder([FromBody] Møder møder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdmin = _usercontext.IsAdmin(this.User);
            if(!isAdmin)
            {
                return null;
            }

            _context.Møder.Add(møder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMøder", new { id = møder.Id }, møder);
        }

        // DELETE: api/Møder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMøder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdmin = _usercontext.IsAdmin(this.User);
            if(!isAdmin)
            {
                return null;
            }

            var møder = await _context.Møder.FindAsync(id);
            if (møder == null)
            {
                return NotFound();
            }

            _context.Møder.Remove(møder);
            await _context.SaveChangesAsync();

            return Ok(møder);
        }

        private bool MøderExists(int id)
        {
            return _context.Møder.Any(e => e.Id == id);
        }
    }
}