using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Configuration
{
    public sealed class SmtpOptions
    {
        public const string SectionName = "Smtp";

        public string Host { get; init; } = default!;
        public int Port { get; init; } = 587;
        public string UserName { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string FromEmail { get; init; } = default!;
        public string FromName { get; init; } = default!;
        public bool EnableSsl { get; init; } = true;
    }
}
