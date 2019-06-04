using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Danrevi.API.Models;
using Danrevi.API.Services;

namespace Danrevi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrugerController:ControllerBase
    {
        private readonly DanreviDbContext _context;
        private readonly IUserContext _usercontext;

        public BrugerController(DanreviDbContext context,IUserContext admin)
        {
            _context = context;
            _usercontext = admin;
        }

        // GET: api/Bruger
        [HttpGet]
        public IEnumerable<Brugere> GetBrugere()
        {
            var isAdmin = _usercontext.IsAdmin(this.User);
            if(!isAdmin)
            {
                return null;
            }
            return _context.Brugere;
        }

        // GET: api/Bruger/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdmin = _usercontext.IsAdmin(this.User);
            if(!isAdmin)
            {
                return null;
            }

            var brugere = await _context.Brugere.FindAsync(id);

            if(brugere == null)
            {
                return NotFound();
            }

            return Ok(brugere);
        }

        // PUT: api/Bruger/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id,[FromBody] Brugere brugere)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdmin = _usercontext.IsAdmin(this.User);
            if(!isAdmin)
            {
                return NoContent();
            }

            if(id != brugere.FirebaseUid)
            {
                return BadRequest();
            }

            _context.Entry(brugere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!UserExists(id))
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

        // POST: api/Bruger
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Brugere brugere)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(UserExists(brugere.FirebaseUid))
            {
                return NoContent();
            }

            var isAdmin = _usercontext.IsAdmin(this.User);
            if(!isAdmin)
            {
                return NoContent();
            }

            _context.Brugere.Add(brugere);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                if(UserExists(brugere.FirebaseUid))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBrugere",new { id = brugere.FirebaseUid },brugere);
        }

        // DELETE: api/Bruger/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdmin = _usercontext.IsAdmin(this.User);
            if(!isAdmin)
            {
                return NoContent();
            }

            var brugere = await _context.Brugere.FindAsync(id);
            if(brugere == null)
            {
                return NotFound();
            }

            _context.Brugere.Remove(brugere);
            await _context.SaveChangesAsync();

            return Ok(brugere);
        }

        private bool UserExists(string id)
        {
            return _context.Brugere.Any(e => e.FirebaseUid == id);
        }
    }
}