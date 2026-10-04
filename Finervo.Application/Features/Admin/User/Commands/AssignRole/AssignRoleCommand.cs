using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Admin.User.Commands.AssignRole
{
    public sealed record AssignRoleCommand(Guid UserId, Guid RoleId, Guid AssignedBy, bool IsSuperAdmin = false) : IRequest<Result>;
}
