using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Queries.GetMyProfile
{
    public sealed record GetMyProfileQuery(Guid Id) : IRequest<Result<UserResponseDto>>;
}
