using Finervo.Contracts.Responses.Role;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Role.Commands.DeleteRole
{
    public sealed record DeleteRoleCommand(Guid Id) : IRequest<Result<RoleDeleteResponseDto>>;
}
