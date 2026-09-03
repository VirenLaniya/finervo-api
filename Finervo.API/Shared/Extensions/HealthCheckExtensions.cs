using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;

namespace Finervo.API.Shared.Extensions
{
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// Registers health checks for API, database etc.
        /// </summary>
        public static IServiceCollection AddInfrastructureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddNpgSql(
                    connectionString: configuration.GetConnectionString("Postgres")!,
                    name: "postgres",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: ["db", "ready"]);

            return services;
        }

        /// <summary>
        /// Maps health check endpoints.
        /// /health        — liveness  — is app running?
        /// /health/ready  — readiness — are dependencies ready?
        /// </summary>
        public static WebApplication UseFinervoHealthChecks(
            this WebApplication app)
        {
            // Liveness — Docker/K8s uses this to know if container is alive
            // Returns 200 if app is running — no dependency checks
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => false,    // ← skip all checks, just return 200
                ResponseWriter = WriteResponse
            }).AllowAnonymous();

            // Readiness — are all dependencies (DB etc) ready?
            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = _ => true,     // ← run all checks
                ResponseWriter = WriteResponse
            }).AllowAnonymous();

            // Detailed — individual dependency status
            app.MapHealthChecks("/health/db", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("db"),
                ResponseWriter = WriteResponse
            }).AllowAnonymous();

            return app;
        }

        private static Task WriteResponse(
            HttpContext context,
            HealthReport report)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var response = new
            {
                Status = report.Status.ToString(),
                Duration = report.TotalDuration.TotalMilliseconds,
                Timestamp = DateTime.UtcNow,
                Checks = report.Entries.Select(e => new
                {
                    Name = e.Key,
                    Status = e.Value.Status.ToString(),
                    Duration = e.Value.Duration.TotalMilliseconds,
                    Error = e.Value.Exception?.Message
                })
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
