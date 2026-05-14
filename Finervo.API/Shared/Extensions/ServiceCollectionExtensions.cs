using Finervo.Application;
using Finervo.Infrastructure;
using Finervo.Contracts;

namespace Finervo.API.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddContracts()
                .AddApplication()
                .AddInfrastructure()
                .AddApiVersioningSetup()   // Api Versioning Configuration Extenstion
                .AddOpenApiWithVersions();  // OpenAPI with Versioning for Documentation

            return services;
        }
    }
}
