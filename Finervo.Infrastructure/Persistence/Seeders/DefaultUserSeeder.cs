using Finervo.Core.Entities;
using Finervo.Core.Interfaces.Security;
using Finervo.Infrastructure.Configuration;
using Finervo.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Finervo.Infrastructure.Persistence.Seeders
{
    /// <summary>
    /// Seeds a single default SuperAdmin user if one does not already exist.
    /// Password is read from configuration key 'DefaultSuperAdmin:Password' or
    /// environment variable 'DEFAULT_SUPERADMIN_PASSWORD'. If password is not provided
    /// the seeder will skip creating the user for security reasons.
    /// </summary>
    public static class DefaultUserSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db, IPasswordHasher passwordHasher, IConfiguration configuration, IOptions<DefaultSuperAdminOptions> options, ILogger<ApplicationDbContext> logger)
        {
            // Read configuration (config overrides environment variables)
            var cfg = options.Value ?? new DefaultSuperAdminOptions();
            var email = cfg.Email ?? Environment.GetEnvironmentVariable("DEFAULT_SUPERADMIN_EMAIL") ?? "superadmin@finervo.local";
            var userName = cfg.UserName ?? Environment.GetEnvironmentVariable("DEFAULT_SUPERADMIN_USERNAME") ?? "superadmin";
            var firstName = cfg.FirstName ?? "Super";
            var lastName = cfg.LastName ?? "Admin";
            var password = cfg.Password ?? Environment.GetEnvironmentVariable("DEFAULT_SUPERADMIN_PASSWORD");

            if (string.IsNullOrWhiteSpace(password))
            {
                logger.LogWarning("Default super admin password not provided. Skipping creation.");
                return;
            }

            // Skip if user already exists
            var existing = await db.Users.FirstOrDefaultAsync(u => u.Email == email || u.UserName == userName);
            if (existing != null)
            {
                logger.LogInformation("Default super admin user already exists. Skipping creation.");
                return;
            }

            // Hash password and create user
            var passwordHash = passwordHasher.Hash(password);
            var createResult = User.Create(firstName, lastName, email, userName, passwordHash);
            if (!createResult.IsSuccess)
            {
                logger.LogError("Failed to create default super admin user: {Error}", createResult.Error);
                return;
            }

            var user = createResult.Data;

            // Find SuperAdmin role
            var role = await db.Roles.FirstOrDefaultAsync(r => r.Name == SystemRoles.SuperAdmin);
            if (role == null)
            {
                logger.LogWarning("SuperAdmin role not found while creating default super admin. Ensure roles are seeded before users.");
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return;
            }

            var assignResult = user.AssignRole(role.Id, user.Id);
            if (!assignResult.IsSuccess)
            {
                logger.LogError("Failed to assign SuperAdmin role to default user: {Error}", assignResult.Error);
                return;
            }

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            logger.LogInformation("Default super admin user created with email {Email}", email);
        }
    }
}
