using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId, string Email) : IDomainEvent;
}
