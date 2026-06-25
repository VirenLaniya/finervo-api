using Finervo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
