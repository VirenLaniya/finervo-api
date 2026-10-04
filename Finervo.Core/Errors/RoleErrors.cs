using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Errors
{
    public static class RoleErrors
    {
        public static readonly Error NotFound =
            Error.NotFound("Role.NotFound", "Role does not exist");

        public static readonly Error InvalidName =
            Error.BadRequest("Role.InvalidName", "Role name is required");

        public static readonly Error InvalidDescription =
            Error.BadRequest("Role.InvalidDescription", "Role description is required");

        public static readonly Error CannotDeactivateSystemRole =
            Error.BadRequest("Role.CannotDeactivateSystemRole", "Built-in system roles cannot be deactivated");

        public static readonly Error CannotDeleteSystemRole =
            Error.BadRequest("Role.CannotDeleteSystemRole", "Built-in system roles cannot be deleted");

        public static readonly Error AlreadyExists =
            Error.Conflict("Role.AlreadyExists", "A role with this name already exists");

        public static readonly Error RoleInactive =
            Error.BadRequest("Role.Inactive", "Cannot assign an inactive role to a user");

        public static readonly Error UnauthorizedSuperAdminAssignment =
            Error.Forbidden("Role.UnauthorizedSuperAdminAssignment", "Only a SuperAdmin can assign the SuperAdmin role to another user");
    }
}
