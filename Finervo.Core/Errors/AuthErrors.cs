using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Errors
{
    public static class AuthErrors
    {
        public static readonly Error TokenExpired =
            Error.Unauthorized("Auth.TokenExpired", "Your session has expired. Please refresh your token or login again.");

        public static readonly Error InvalidToken =
            Error.Unauthorized("Auth.InvalidToken", "The provided token is invalid.");

        public static readonly Error Unauthorized =
            Error.Unauthorized("Auth.Unauthorized", "Authentication is required to access this resource.");

        public static readonly Error Forbidden =
            Error.Forbidden("Auth.Forbidden", "You do not have permission to access this resource.");
    }
}
