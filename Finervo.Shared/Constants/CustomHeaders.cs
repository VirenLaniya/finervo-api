using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Shared.Constants
{
    /// <summary>
    /// Defines custom HTTP header names used across the Finervo platform.
    /// Centralizing header names prevents magic strings and ensures consistency
    /// across middleware, controllers, and external service clients.
    /// </summary>
    public static class CustomHeaders
    {
        /// <summary>
        /// Unique identifier per HTTP request.
        /// Used to correlate all log entries belonging to the same request.
        /// Clients can provide this header or one will be generated automatically.
        /// Example: "X-Correlation-Id: a1b2c3d4-e5f6-7890-abcd-ef1234567890"
        /// </summary>
        public const string CorrelationId = "X-Correlation-Id";
    }
}
