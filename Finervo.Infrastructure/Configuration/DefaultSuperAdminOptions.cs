using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Configuration
{
    public sealed class DefaultSuperAdminOptions
    {
        public const string SectionName = "DefaultSuperAdmin";

        public string? Email { get; init; }
        public string? UserName { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Password { get; init; }
    }
}
