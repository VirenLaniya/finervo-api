using Finervo.Core.Interfaces.Security;
using Finervo.Infrastructure.Configuration;
using Finervo.Infrastructure.Persistence;
using Finervo.Infrastructure.Persistence.Seeders;
using Microsoft.Extensions.Options;

namespace Finervo.API.Shared.Extensions;

/// <summary>
/// Handles database seeding on application startup.
/// Seeders are idempotent — safe to run on every startup.
/// </summary>
public static class SeederExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider
            .GetRequiredService<ILogger<ApplicationDbContext>>();

        try
        {
            logger.LogInformation("Starting database seeding");

            await RoleSeeder.SeedAsync(db);

            // Seed default administrative user (reads password from configuration or environment)
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var defaultSuperAdminOptions = scope.ServiceProvider.GetRequiredService<IOptions<DefaultSuperAdminOptions>>();
            await DefaultUserSeeder.SeedAsync(db, passwordHasher, configuration, defaultSuperAdminOptions, logger);

            logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database seeding failed");
            throw;
        }
    }
}