using Finervo.Core.Entities;
using Finervo.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Persistence.Seeders
{
    /// <summary>
    /// Seeds built-in roles and permissions from FinervoRoles definitions.
    /// Runs on startup — safe to run multiple times (idempotent).
    /// New permissions added in code are automatically picked up on next startup.
    /// </summary>
    public static class RoleSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            await SeedRolesAsync(db);

            await db.SaveChangesAsync();
        }

        private static async Task SeedRolesAsync(ApplicationDbContext db)
        {
            foreach (var definition in SystemRoles.Definitions)
            {
                var existingRole = await db.Set<Role>()
                    .FirstOrDefaultAsync(r => r.Name == definition.Name);

                if(existingRole == null)
                {
                    var newRole = Role.Create(
                            definition.Name,
                            definition.Description,
                            definition.IsSystem);

                    if (!newRole.IsSuccess) return;

                    await db.Set<Role>().AddAsync(newRole.Data);
                }
            }
        }
    }
}
