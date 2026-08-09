using Finervo.Core.Entities;

namespace Finervo.Core.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken ct = default);
        Task<User?> GetUserByUserNameAsync(string username, CancellationToken ct = default);
        Task<bool> ExistsByUsernameAsync(string username, Guid? excludedUserId = null, CancellationToken ct = default);
        Task<bool> ExistsByEmailAsync(string email, Guid? excludedUserId = null, CancellationToken ct = default);
    }
}
