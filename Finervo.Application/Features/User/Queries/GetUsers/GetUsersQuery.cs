using Finervo.Contracts.Responses.User;
using Finervo.Core.Interfaces;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Queries.GetUsers
{
    public sealed record GetUsersQuery : IRequest<Result<IEnumerable<UserResponseDto>>>
    {
    }
}
