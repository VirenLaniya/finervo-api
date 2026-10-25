using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Common.Interfaces
{
    /// <summary>
    /// Defines email sending operations for the Finervo platform.
    /// Implementation details (SMTP provider) live in Infrastructure.
    /// </summary>
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(
            string toEmail,
            string toName,
            string verificationLink,
            DateTime tokenExpiry,
            CancellationToken ct = default);

        Task SendWelcomeEmailAsync(
            string toEmail,
            string toName,
            string loginLink,
            CancellationToken ct = default);
    }
}
