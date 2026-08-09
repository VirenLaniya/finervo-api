
namespace Finervo.Contracts.Responses.Auth
{
    public sealed record LoginResponseDto(
        Guid UserId,
        string Email,
        string AccessToken,
        string RefreshToken
        );
}
