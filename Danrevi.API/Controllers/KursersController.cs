using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Danrevi.API.Models;
using Danrevi.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace Danrevi.API.Controllers
{
    [Route("api/kurser")]
    [ApiController]
    [Authorize]
    public class KursersController:ControllerBase
    {
        private readonly DanreviDbContext _context;
        private readonly IUserContext _usercontext;

        public KursersController(DanreviDbContext context,IUserContext admin)
        {
            _context = context;
            _usercontext = admin;
        }

        //lave et interface IUserAdmin
        //inject shit i DI
        //IUserAdmin med i ctor på alle controller
        //pbulic admin: iuseradmin
        //ctor (DanreviDbContext context)
        //IUserAdmin har en method IsAdmin()
        /*
         *public bool IsAdmin(ClaimsPrincipal user) {
         * var userid = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.Brugere.Single(x => x.FirebaseUid.CompareTo(userid) == 0);
            return user.IsAdmin
         */

        // GET: api/Kursers
        [HttpGet]
        public IEnumerable<Kurser> GetKurser()
        {
            return _context.Kurser;
        }

        // GET: api/Kursers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKurser([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var kurser = await _context.Kurser.FindAsync(id);

            if(kurser == null)
            {
                return NotFound();
            }

            return Ok(kurser);
        }

        // PUT: api/Kursers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKurser([FromRoute] int id,[FromBody] Kurser kurser)
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

            if(id != kurser.Id)
            {
                return BadRequest();
            }

            _context.Entry(kurser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!KurserExists(id))
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

        // POST: api/Kursers
        [HttpPost]
        public async Task<IActionResult> PostKurser([FromBody] Kurser kurser)
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

            _context.Kurser.Add(kurser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKurser",new { id = kurser.Id },kurser);
        }

        // DELETE: api/Kursers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKurser([FromRoute] int id)
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

            var kurser = await _context.Kurser.FindAsync(id);
            if(kurser == null)
            {
                return NotFound();
            }

            _context.Kurser.Remove(kurser);
            await _context.SaveChangesAsync();

            return Ok(kurser);
        }

        private bool KurserExists(int id)
        {
            return _context.Kurser.Any(e => e.Id == id);
        }
    }
}