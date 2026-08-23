using Finervo.API.Shared.Middlewares;

namespace Finervo.API.Shared.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseCustomMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<RequestContextMiddleware>();
            app.UseRequestLogging();

            return app;
        }
    }
}
