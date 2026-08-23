using Finervo.API.Shared.Helpers;
using Finervo.Application.Common.Interfaces;
using Finervo.Infrastructure.Services;
using Finervo.Shared.Constants;
using Serilog.Context;
using System.Security.Claims;

namespace Finervo.API.Shared.Middlewares
{
    public class RequestContextMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// Enriches every log entry in a request with a CorrelationId and UserId,
        /// making it possible to trace all logs for a single request across concurrent traffic.
        /// </summary>
        public async Task InvokeAsync(HttpContext context, IRequestContext requestContext)
        {
            
            var correlationId = CorrelationIdHelper.GetOrGenerate(context);
            var userId = UserIdHelper.GetOrDefault(context);

            requestContext.Initialize(correlationId, userId);

            // OnStarting — fires just before headers sent, always safe
            context.Response.OnStarting(() =>
            {
                // Add to response headers so client can trace too
                context.Response.Headers[CustomHeaders.CorrelationId] = correlationId;
                return Task.CompletedTask;
            });

            // Push into Serilog LogContext — applies to ALL logs in this request
            using (LogContext.PushProperty(LogContextProperties.CorrelationId, correlationId))
            using (LogContext.PushProperty(LogContextProperties.UserId, userId))
            using (LogContext.PushProperty(LogContextProperties.RequestPath, context.Request.Path))
            using (LogContext.PushProperty(LogContextProperties.RequestMethod, context.Request.Method))
            {
                await next(context);
            }
        }
    }
}
