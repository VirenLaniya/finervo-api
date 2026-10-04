using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Role.Commands.UpdateRole
{
    public sealed record UpdateRoleCommand(Guid Id, string Description, bool IsActive) : IRequest<Result<bool>>;
}
