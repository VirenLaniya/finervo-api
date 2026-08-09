using Finervo.Core.Entities;
using Finervo.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finervo.Infrastructure.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        public async Task<User?> GetUserByUserNameAsync(string username, CancellationToken ct = default)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == username, ct);
        }

        public async Task<bool> ExistsByUsernameAsync(string username, Guid? excludedUserId = null, CancellationToken ct = default)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username && (!excludedUserId.HasValue || u.Id != excludedUserId), ct);
        }

        public async Task<bool> ExistsByEmailAsync(string email, Guid? excludedUserId = null, CancellationToken ct = default)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && (!excludedUserId.HasValue || u.Id != excludedUserId), ct);
        }
    }
}
