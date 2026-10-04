using Finervo.Core.Entities;
using Finervo.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Persistence.Repositories
{
    public class RoleRepository(ApplicationDbContext context) : GenericRepository<Role>(context), IRoleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludedRoleId = null, CancellationToken ct = default)
        {
            return await _context.Roles.AnyAsync(r => r.Name == name && (!excludedRoleId.HasValue || r.Id != excludedRoleId), ct);
        }

        public async Task<Role?> GetByNameAsync(string name, CancellationToken ct = default)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name, ct);
        }
    }
}
