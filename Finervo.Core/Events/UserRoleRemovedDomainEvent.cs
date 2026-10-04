using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Events
{
    public sealed record UserRoleRemovedDomainEvent(
        Guid UserId,
        Guid RoleId
    ) : IDomainEvent;
}
