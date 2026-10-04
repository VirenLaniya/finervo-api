using Finervo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Interfaces.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name, CancellationToken ct = default);
        Task<bool> ExistsByNameAsync(string name, Guid? excludedRoleId = null, CancellationToken ct = default);
    }
}
