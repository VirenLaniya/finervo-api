using Finervo.Shared.Constants;

namespace Finervo.API.Shared.Helpers
{
    /// <summary>
    /// Resolves or generates a correlation ID from the incoming HTTP request.
    /// If the client provides an X-Correlation-Id header, that value is used.
    /// Otherwise a new GUID is generated to uniquely identify the request.
    /// </summary>
    public static class CorrelationIdHelper
    {
        /// <summary>
        /// Returns the correlation ID from the request header if present,
        /// otherwise generates a new unique identifier.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>A non-empty correlation ID string.</returns>
        public static string GetOrGenerate(HttpContext context)
        {
            var correlationId = context.Request.Headers[CustomHeaders.CorrelationId].FirstOrDefault();

            return string.IsNullOrWhiteSpace(correlationId) 
                ? Guid.NewGuid().ToString() 
                : correlationId;
        }
    }
}
