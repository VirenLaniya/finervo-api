using Finervo.Contracts.Responses.Role;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Role.Queries.GetRoles
{
    public sealed record GetRolesQuery : IRequest<Result<IEnumerable<RoleResponseDto>>>;
}
