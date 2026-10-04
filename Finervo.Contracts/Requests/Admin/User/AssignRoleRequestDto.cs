using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Contracts.Requests.Admin.User
{
    public sealed record AssignRoleRequestDto(Guid RoleId);
}
