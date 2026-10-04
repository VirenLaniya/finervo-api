using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Contracts.Requests.Role
{
    public record UpdateRoleRequestDto(string Description, bool IsActive);
}
