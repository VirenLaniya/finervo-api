using Finervo.Contracts.Responses.Role;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Role.Commands.AddRole
{
    public sealed record AddRoleCommand(string Name, string Description) : IRequest<Result<RoleResponseDto>>;
}
