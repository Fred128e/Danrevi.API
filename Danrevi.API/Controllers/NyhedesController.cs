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
    [Route("api/nyheder")]
    [ApiController]
    [Authorize]
    public class NyhedesController : ControllerBase
    {
        private readonly DanreviDbContext _context;
        private readonly IUserContext _usercontext;


        public NyhedesController(DanreviDbContext context, IUserContext admin)
        {
            _context = context;
            _usercontext = admin;
        }

        // GET: api/Nyhedes
        [HttpGet]
        public IEnumerable<Nyheder> GetNyheder()
        {
            return _context.Nyheder;
        }

        // GET: api/Nyhedes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNyheder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nyheder = await _context.Nyheder.FindAsync(id);

            if (nyheder == null)
            {
                return NotFound();
            }

            return Ok(nyheder);
        }

        // PUT: api/Nyhedes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNyheder([FromRoute] int id, [FromBody] Nyheder nyheder)
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

            if (id != nyheder.Id)
            {
                return BadRequest();
            }

            _context.Entry(nyheder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NyhederExists(id))
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

        // POST: api/Nyhedes
        [HttpPost]
        public async Task<IActionResult> PostNyheder([FromBody] Nyheder nyheder)
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

            _context.Nyheder.Add(nyheder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNyheder", new { id = nyheder.Id }, nyheder);
        }

        // DELETE: api/Nyhedes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNyheder([FromRoute] int id)
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

            var nyheder = await _context.Nyheder.FindAsync(id);
            if (nyheder == null)
            {
                return NotFound();
            }

            _context.Nyheder.Remove(nyheder);
            await _context.SaveChangesAsync();

            return Ok(nyheder);
        }

        private bool NyhederExists(int id)
        {
            return _context.Nyheder.Any(e => e.Id == id);
        }
    }
}