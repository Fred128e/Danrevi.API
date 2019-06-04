using System.Security.Claims;

namespace Danrevi.API.Services
{
    public interface IUserContext
    {
        bool IsAdmin(ClaimsPrincipal user);
    }
}