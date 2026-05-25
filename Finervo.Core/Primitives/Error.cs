using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Primitives
{
    public sealed record Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.Null", "Null value provided");
    }
}
