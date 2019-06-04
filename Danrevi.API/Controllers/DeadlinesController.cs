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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeadlinesController : ControllerBase
    {
        private readonly DanreviDbContext _context;
        private readonly IUserContext _usercontext;

        public DeadlinesController(DanreviDbContext context, IUserContext admin)
        {
            _context = context;
            _usercontext = admin;
        }

        // GET: api/Deadlines
        [HttpGet]
        public IEnumerable<Deadline> GetDeadline()
        {
            return _context.Deadline;
        }

        // GET: api/Deadlines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeadline([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deadline = await _context.Deadline.FindAsync(id);

            if (deadline == null)
            {
                return NotFound();
            }

            return Ok(deadline);
        }

        // PUT: api/Deadlines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeadline([FromRoute] int id, [FromBody] Deadline deadline)
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

            if (id != deadline.Id)
            {
                return BadRequest();
            }

            _context.Entry(deadline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeadlineExists(id))
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

        // POST: api/Deadlines
        [HttpPost]
        public async Task<IActionResult> PostDeadline([FromBody] Deadline deadline)
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

            _context.Deadline.Add(deadline);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeadline", new { id = deadline.Id }, deadline);
        }

        // DELETE: api/Deadlines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeadline([FromRoute] int id)
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

            var deadline = await _context.Deadline.FindAsync(id);
            if (deadline == null)
            {
                return NotFound();
            }

            _context.Deadline.Remove(deadline);
            await _context.SaveChangesAsync();

            return Ok(deadline);
        }

        private bool DeadlineExists(int id)
        {
            return _context.Deadline.Any(e => e.Id == id);
        }
    }
}