using Finervo.Core.Primitives;

namespace Finervo.Core.Events
{
    public sealed record UserRegisteredDomainEvent(Guid UserId, string Email, string FirstName, string VerificationToken, DateTime TokenExpiry) : IDomainEvent;
}
