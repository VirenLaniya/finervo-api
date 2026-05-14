using Asp.Versioning.ApiExplorer;
using Finervo.API.Shared.OpenApi;
using Microsoft.OpenApi;
using System.Reflection.Metadata;

namespace Finervo.API.Shared.Extensions
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection AddOpenApiWithVersions(this IServiceCollection services)
        {

            IReadOnlyList<ApiVersionInfo> versions = ApiVersionProvider.GetApiVersions();

            services.AddOpenApi();

            foreach (var version in versions)
            {
                services.AddOpenApi(version.GroupName, options =>
                {
                    options.ShouldInclude = description =>
                    {
                        return description.GroupName == version.GroupName;
                    };

                    options.AddDocumentTransformer((document, context, cancellationToken) =>
                    {
                        document.Info = new()
                        {
                            Title = version.Title,
                            Version = version.Version.ToString(),
                            Description = version.Deprecated ? $"[DEPRECATED] {version.Description}" : version.Description
                        };

                        return Task.CompletedTask;
                    });
                });
            }

            return services;
        }

        public static IApplicationBuilder UseOpenApiWithSwagger(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment()) return app;

            IReadOnlyList<ApiVersionInfo> versions = ApiVersionProvider.GetApiVersions();

            app.MapOpenApi("/openapi/{documentName}.json");

            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "Finervo API Explorer | Swagger";

                foreach (var version in versions)
                {
                    options.RoutePrefix = "docs/swagger";

                    options.SwaggerEndpoint(
                        url: $"/openapi/{version.GroupName}.json",
                        name: $"{version.GroupName} : {version.Title} {(version.Deprecated ? " (Deprecated)" : "")}"
                        );
                }
            });

            return app;
        }
    }
}
