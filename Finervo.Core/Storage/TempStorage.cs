using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Storage
{
    public class TempStorage
    {
        public static readonly List<string> _summaries = [];

        public static IReadOnlyList<string> Summaries => _summaries.AsReadOnly();

        public static void AddSummary(string summary) =>
            _summaries.Add(summary);

        public static bool Exists(string summary) =>
            _summaries.Contains(summary);
    }
}
