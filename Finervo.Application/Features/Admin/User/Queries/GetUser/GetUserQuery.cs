using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.Admin.User.Queries.GetUser
{
    public sealed record GetUserQuery(Guid Id) : IRequest<Result<UserResponseDto>>;
}
