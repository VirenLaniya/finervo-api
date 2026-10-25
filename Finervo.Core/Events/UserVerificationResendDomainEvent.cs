using Finervo.Core.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Events
{
    public sealed record UserVerificationResendDomainEvent(
        Guid UserId,
        string Email,
        string FirstName,
        string VerificationToken,
        DateTime TokenExpiry
    ) : IDomainEvent;
}
