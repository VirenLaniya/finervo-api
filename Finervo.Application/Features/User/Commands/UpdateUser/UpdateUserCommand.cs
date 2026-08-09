using Finervo.Contracts.Requests.User;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.User.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(Guid Id, string FirstName, string LastName, string UserName, string Email) : IRequest<Result<bool>>
    {
        // Factory method to map request DTO fields with command fields
        public static UpdateUserCommand FromRequest(Guid id, UpdateUserRequestDto dto) =>
            new(
                id,
                dto.FirstName,
                dto.LastName,
                dto.UserName,
                dto.Email);
    }
}
