using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Events
{
    public sealed record UserEmailVerifiedDomainEvent(Guid UserId, string Email, string FirstName) : IDomainEvent;
}
