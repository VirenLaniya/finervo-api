using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.User.Queries.GetUser
{
    public sealed record GetUserQuery(Guid id) : IRequest<Result<UserResponseDto>>;
}
