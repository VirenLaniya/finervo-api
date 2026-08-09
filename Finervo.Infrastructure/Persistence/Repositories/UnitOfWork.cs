using Finervo.Core.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Persistence.Repositories
{
    internal sealed class UnitOfWork(ApplicationDbContext db) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await db.SaveChangesAsync(ct);
        }
    }
}
