
namespace Finervo.Core.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
