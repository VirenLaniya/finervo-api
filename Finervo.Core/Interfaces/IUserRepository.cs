using Finervo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
    }
}
