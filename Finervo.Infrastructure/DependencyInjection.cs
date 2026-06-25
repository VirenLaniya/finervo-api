using Finervo.Core;
using Finervo.Core.Interfaces;
using Finervo.Infrastructure.Persistence;
using Finervo.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finervo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Postgres");

            services.AddDbContext<ApplicationDbContext>(options =>
        options
            .UseNpgsql(connectionString, npgsql =>
            {
                // Retry on transient failures
                npgsql.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorCodesToAdd: null);

                // Migration assembly
                npgsql.MigrationsAssembly(
                    typeof(ApplicationDbContext).Assembly.FullName);
            })
            // Log SQL queries in development
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
