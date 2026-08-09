using Finervo.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Finervo.Infrastructure.Authentication
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public Guid? UserId
        {
            get
            {
                var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(userId, out Guid id) ? id : null;
            }
        }

        public string? Email =>
            httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Email);

        public bool IsAuthenticated =>
            httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
}
