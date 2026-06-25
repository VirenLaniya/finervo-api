using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
