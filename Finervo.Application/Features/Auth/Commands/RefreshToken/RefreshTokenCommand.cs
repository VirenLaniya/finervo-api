using Finervo.Contracts.Responses.Auth;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.Auth.Commands.RefreshToken
{
    public sealed record RefreshTokenCommand(Guid UserId, string RefreshToken) : IRequest<Result<RefreshTokenResponseDto>>;
}
