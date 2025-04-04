using System.Security.Claims;
using Infrastructure.Interfaces;

namespace Presentation.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor)
        : IUser
    {
        public int Id
        {
            get
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
                if (int.TryParse(userIdClaim, out var userId))
                {
                    return userId;
                }

                throw new UnauthorizedAccessException("ID assignment failed.");
            }
        }
    }
}