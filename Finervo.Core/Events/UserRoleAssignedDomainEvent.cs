using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Events
{
    public sealed record UserRoleAssignedDomainEvent(
        Guid UserId,
        Guid RoleId,
        Guid AssignedBy
    ) : IDomainEvent;
}
