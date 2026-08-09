using Finervo.Core.Errors;
using Finervo.Core.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace Finervo.Infrastructure.Authentication
{
    public static class CustomJwtBearerEvents
    {
        // Fires when token validation fails (expired, invalid signature etc)
        public static Task OnChallenge(JwtBearerChallengeContext context)
        {
            // Suppress default WWW-Authenticate header response
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            // Determine specific error
            var error = context.AuthenticateFailure switch
            {
                { } ex when ex.Message.Contains("expired") =>
                    AuthErrors.TokenExpired,

                { } ex when ex.Message.Contains("signature") =>
                    AuthErrors.InvalidToken,

                _ =>
                    AuthErrors.Unauthorized
            };

            var response = Result.Failure(error);

            return context.Response.WriteAsJsonAsync(response);
        }

        // Fires when authenticated user lacks permission (wrong role etc)
        public static Task OnForbidden(ForbiddenContext context)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var response = Result.Failure(AuthErrors.Forbidden);

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
