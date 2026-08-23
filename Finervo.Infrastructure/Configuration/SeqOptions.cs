using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Configuration
{
    public sealed class SeqOptions
    {
        public const string SectionName = "Seq";

        public string ServerUrl { get; init; } = default!;
        public string ApiKey { get; init; } = string.Empty;  // optional for local
        public bool Enabled { get; init; } = true;
    }
}
