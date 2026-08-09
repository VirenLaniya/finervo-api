using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.Auth.Commands.Register
{
    public sealed record RegisterCommand(string FirstName, string LastName, string Email, string Password, string ConfirmPassword) : IRequest<Result<UserResponseDto>>;
}
