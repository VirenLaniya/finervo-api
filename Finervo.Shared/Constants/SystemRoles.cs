using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Shared.Constants
{
    /// <summary>
    /// Defines all built-in roles for the Finervo platform.
    /// These roles are seeded on application startup and cannot be deleted.
    /// </summary>
    public static class SystemRoles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string User = "User";

        /// <summary>
        /// Role metadata — name, description and permissions per role.
        /// Used for seeding and display purposes.
        /// </summary>
        public static readonly IReadOnlyList<RoleDefinition> Definitions =
        [
            new(
                Name:        SuperAdmin,
                Description: "Full system access. Manages all users, roles and platform configuration. Cannot be deleted or deactivated.",
                IsSystem:    true,
                Permissions: []
            ),
            new(
                Name:        Admin,
                Description: "Administrative access. Manages users and views all expenses, budgets and reports within the platform.",
                IsSystem:    true,
                Permissions: []
            ),
            new(
                Name:        User,
                Description: "Standard user access. Manages own expenses and budgets, views personal reports.",
                IsSystem:    true,
                Permissions: []
            )
        ];
    }

    /// <summary>
    /// Represents a role definition used for seeding and display.
    /// </summary>
    /// <param name="Name">Unique role identifier.</param>
    /// <param name="Description">Human-readable description of the role's purpose and access level.</param>
    /// <param name="IsSystem">If true, this role cannot be deleted or deactivated.</param>
    /// <param name="Permissions">List of permission strings assigned to this role.</param>
    public sealed record RoleDefinition(
        string Name,
        string Description,
        bool IsSystem,
        string[] Permissions
    );
}
