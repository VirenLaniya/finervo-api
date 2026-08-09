using Finervo.Contracts.Requests.User;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.DeleteUser
{
    public sealed record DeleteUserCommand(Guid Id) : IRequest<Result<UserResponseDto>>;
}
