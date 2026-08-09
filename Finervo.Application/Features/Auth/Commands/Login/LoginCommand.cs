using Finervo.Contracts.Responses.Auth;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.Auth.Commands.Login
{
    public sealed record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponseDto>>;
}
