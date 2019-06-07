using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Danrevi.API.Models;

namespace Danrevi.API.Services
{
    public class UserContext:IUserContext
    {
        private readonly DanreviDbContext _context = null;
        public UserContext(DanreviDbContext context)
        {
            _context = context;
        }
        public bool IsAdmin(ClaimsPrincipal user)
        {
            var userid = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = _context.Brugere.Single(x => x.FirebaseUid.CompareTo(userid) == 0);
            return currentUser.IsAdmin;
        }

        public string UserId(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

       
    }
}
