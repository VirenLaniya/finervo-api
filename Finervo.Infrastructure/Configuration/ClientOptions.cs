using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Configuration
{
    /// <summary>
    /// Frontend client URL configuration.
    /// Used to generate links sent in emails.
    /// </summary>
    public sealed class ClientOptions
    {
        public const string SectionName = "Client";

        public string BaseUrl { get; init; } = default!;

        #region Computed Properties

        public string EmailVerificationUrl => $"{BaseUrl}/verify-email";
        public string LoginUrl => $"{BaseUrl}/login";

        #endregion
    }
}
