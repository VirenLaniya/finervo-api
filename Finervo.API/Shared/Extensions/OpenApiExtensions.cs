using Asp.Versioning.ApiExplorer;
using Finervo.API.Shared.OpenApi;
using Finervo.API.Shared.OpenApi.Transformers;
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

                        document.Components ??= new OpenApiComponents();

                        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
                        {
                            // Define Bearer scheme
                            {
                                "Bearer",
                                new OpenApiSecurityScheme
                                {
                                    Type = SecuritySchemeType.Http,
                                    Scheme = "bearer",
                                    BearerFormat = "JWT",
                                    In = ParameterLocation.Header,
                                    Name = "Authorization",
                                    Description = "Enter your JWT access token. Example: eyJhbGci..."
                                }
                            }
                        };

                        // Apply globally to all operations
                        var securityRequirement = new OpenApiSecurityRequirement
                            {
                                {
                                    new OpenApiSecuritySchemeReference("Bearer", document),
                                    new List<string>()
                                }
                            };

                        foreach (var path in document.Paths.Values)
                        {
                            if (path?.Operations is not null)
                            {
                                foreach (var operation in path.Operations.Values)
                                {
                                    operation.Security ??= [];
                                    operation.Security.Add(securityRequirement);
                                }
                            }
                        }

                        return Task.CompletedTask;
                    });

                    options.AddOperationTransformer<CorrelationIdHeaderTransformer>();
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

                    options.DisplayRequestDuration();

                    options.EnableFilter();

                    options.EnableDeepLinking();
                }
            });

            return app;
        }
    }
}
