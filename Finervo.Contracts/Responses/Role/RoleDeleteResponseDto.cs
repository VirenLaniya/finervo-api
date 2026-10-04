using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Contracts.Responses.Role
{
    public sealed record RoleDeleteResponseDto(Guid Id, string Name);
}
