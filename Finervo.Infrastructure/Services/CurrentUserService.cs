using Finervo.Application.Common.Interfaces;
using Finervo.Shared.Constants;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Finervo.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private ClaimsPrincipal? User =>
            httpContextAccessor.HttpContext?.User;

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

        public IReadOnlyList<string> Roles =>
            User?.FindAll(ClaimTypes.Role)
             .Select(c => c.Value)
             .ToList() ?? [];

        public bool IsAuthenticated =>
            httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string role) =>
            Roles.Contains(role, StringComparer.OrdinalIgnoreCase);

        public bool IsSuperAdmin =>
            IsInRole(SystemRoles.SuperAdmin);
    }
}
