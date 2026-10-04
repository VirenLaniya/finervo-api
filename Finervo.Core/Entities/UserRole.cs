using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Entities
{
    /// <summary>
    /// Join entity representing the assignment of a role to a user.
    /// Uses composite key (UserId + RoleId) — no surrogate Id needed.
    /// </summary>
    public sealed class UserRole
    {
        #region Fields

        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }
        public Guid AssignedBy { get; private set; }
        public DateTime AssignedAt { get; private set; }

        #endregion

        #region Navigation Properties

        public User? User { get; private set; }
        public Role? Role { get; private set; }

        #endregion

        #region Constructor

        private UserRole() { }

        #endregion

        #region Methods

        public static UserRole Create(Guid userId, Guid roleId, Guid assignedBy) =>
            new()
            {
                UserId = userId,
                RoleId = roleId,
                AssignedBy = assignedBy,
                AssignedAt = DateTime.UtcNow
            };

        #endregion
    }
}
