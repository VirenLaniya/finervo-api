using System.Net;
using System.Text.Json.Serialization;

namespace Finervo.Core.Primitives
{
    public sealed record Error(string Code, string Message, [property: JsonIgnore] HttpStatusCode HttpStatuscode = HttpStatusCode.BadRequest)
    {
        #region Factory methods

        public static Error NotFound(string code, string message) =>
            new(code, message, HttpStatusCode.NotFound);

        public static Error UnprocessableEntity(string code, string message) =>
            new(code, message, HttpStatusCode.UnprocessableEntity);

        public static Error Unauthorized(string code, string message) =>
            new(code, message, HttpStatusCode.Unauthorized);

        public static Error Conflict(string code, string message) =>
            new(code, message, HttpStatusCode.Conflict);

        public static Error Forbidden(string code, string message) =>
            new(code, message, HttpStatusCode.Forbidden);

        public static Error BadRequest(string code, string message) =>
            new(code, message, HttpStatusCode.BadRequest);

        public static Error ServerError(string code, string message) =>
            new(code, message, HttpStatusCode.InternalServerError);

        public static Error Validation(string message) =>
            new("Validation.Failed", message, HttpStatusCode.BadRequest);

        public static Error NotImplemented(string message) =>
            new("Error.NotImplemented", message, HttpStatusCode.NotImplemented);

        #endregion
    }
}
