using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Contracts.Responses.Role
{
    public sealed record RoleResponseDto(Guid Id, string Name, string Description, bool IsActive, DateTime LastUpdatedAt);
}
