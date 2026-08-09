using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.Auth.Commands.Logout
{
    public sealed record LogoutCommand(Guid UserId) : IRequest<Result>;
}
