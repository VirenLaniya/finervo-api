using Finervo.Core.Errors;
using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Entities
{
    public class Role : AggregateRoot
    {
        #region Fields

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// True for system built-in roles (User, Admin etc) - Cannot delete
        /// </summary>
        public bool IsSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        #endregion

        #region Constructor

        public Role(Guid id, string name, string description, bool isSystem) : base(id)
        {
            Name = name;
            Description = description;
            IsActive = true;
            IsSystem = isSystem;
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
        }

        #endregion

        #region Methods

        public static Result<Role> Create(string name, string description, bool isSystem = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result<Role>.Failure(RoleErrors.InvalidName);

            if (string.IsNullOrWhiteSpace(description))
                return Result<Role>.Failure(RoleErrors.InvalidDescription);

            return Result<Role>.Success(new(Guid.NewGuid(), name, description, isSystem));
        }

        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) return;
                Description = description;
        }

        public Result Deactivate()
        {
            if (IsSystem)
                return Result.Failure(RoleErrors.CannotDeactivateSystemRole);

            IsActive = false;
            return Result.Success();
        }

        #endregion
    }
}
