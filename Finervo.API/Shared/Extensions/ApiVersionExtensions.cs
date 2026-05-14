using Asp.Versioning;
using Finervo.API.Shared.OpenApi;

namespace Finervo.API.Shared.Extensions
{
    public static class ApiVersionExtensions
    {
        public static IServiceCollection AddApiVersioningSetup(this IServiceCollection services)
        {
            ApiVersionInfo defaultVersion = ApiVersionProvider.GetDefaultApiVersion();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = defaultVersion.Version;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
