using Finervo.Contracts.Requests.User;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.UpdateUser
{
    public sealed record DeleteUserCommand(int Id) : IRequest<Result<UserResponseDto>>
    {
    }
}
