
namespace Finervo.Contracts.Responses.Auth
{
    public sealed record RefreshTokenResponseDto(
        Guid UserId,
        string AccessToken,
        string RefreshToken
        );
}
