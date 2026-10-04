using Finervo.Core.Entities;

namespace Finervo.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, IEnumerable<string> roles);
        string GenerateRefreshToken();
    }
}
