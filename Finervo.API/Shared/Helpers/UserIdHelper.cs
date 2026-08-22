using System.Security.Claims;

namespace Finervo.API.Shared.Helpers
{
    public static class UserIdHelper
    {
        /// <summary>
        /// Returns the UserId from JWT claims if authenticated,
        /// otherwise 'anonymous'
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>A non-empty string, User ID or anonymous.</returns>
        public static string GetOrDefault(HttpContext context)
        {
            return context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
        }
    }
}
