using Finervo.Contracts.Requests.User;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(UpdateUserRequestDto updateUserRequestDto) : IRequest<Result<bool>>
    {
    }
}
