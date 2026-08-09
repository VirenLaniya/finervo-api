using Finervo.Core.Errors;
using Finervo.Core.Primitives;
using Finervo.Shared.Extensions;
using FluentValidation;
using System.Net;
using System.Net.Mime;

namespace Finervo.API.Shared.Middlewares
{
    /// <summary>
    /// Middleware that globally handles unhandled exceptions across the application pipeline.
    /// Catches <see cref="ValidationException"/> and general <see cref="Exception"/> types,
    /// returning structured JSON error responses instead of exposing raw exception details.
    /// </summary>
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// Invokes the middleware and processes the HTTP request.
        /// Catches any unhandled exceptions thrown during request execution
        /// and delegates to the appropriate handler based on exception type.
        /// </summary>
        /// <param name="context">The current HTTP context for the request.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
			try
			{
                await next(context);
			}
			catch (ValidationException ex)
			{
                // TO DO : Add logger
                await HandleValidationExceptionAsync(context, ex);
			}
            catch(Exception ex)
            {
                // TO DO : Add logger
                Console.WriteLine(ex.Message);
                await HandleExceptionAsync(context);
            }
        }

        /// <summary>
        /// Handles <see cref="ValidationException"/> thrown by the MediatR validation pipeline.
        /// Returns a structured <c>400 Bad Request</c> response containing field-level
        /// validation errors grouped by property name.
        /// </summary>
        /// <param name="context">The current HTTP context used to write the response.</param>
        /// <param name="ex">The <see cref="ValidationException"/> containing validation failure details.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous write operation.</returns>
        public static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key.ToCamelCase(),
                    g => g.Select(e => e.ErrorMessage).ToArray()
                    );

            var response = ValidationErrorResult.WithErrors(errors);

            await context.Response.WriteAsJsonAsync(response);
        }

        /// <summary>
        /// Handles any unhandled <see cref="Exception"/> that is not a validation error.
        /// Logs the error and returns a generic <c>500 Internal Server Error</c> response
        /// without exposing internal exception details to the client.
        /// </summary>
        /// <param name="context">The current HTTP context used to write the response.</param>
        /// <param name="ex">The unhandled <see cref="Exception"/> that was thrown.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous write operation.</returns>
        public static async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var response = Result.Failure(CommonErrors.UnexpectedServerError);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
