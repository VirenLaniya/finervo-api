using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Finervo.Core.Errors
{
    public static class CommonErrors
    {
        public static readonly Error None =
            new(string.Empty, string.Empty, HttpStatusCode.OK);

        public static readonly Error NullValue =
            Error.UnprocessableEntity("Error.Null", "Null value provided");

        public static readonly Error UnexpectedServerError =
            Error.ServerError("Server.Error", "An unexpected error occurred.");

        public static readonly Error ValidationFailed =
            Error.Validation("One or more validation errors occurred");

        public static readonly Error NotImplemented =
            Error.NotImplemented("This feature is not yet implemented.");
    }
}
