
namespace Finervo.Contracts.Requests.Auth
{
    public sealed record RefreshTokenRequestDto(Guid UserId, string RefreshToken);
}
