
namespace Finervo.Contracts.Requests.Auth
{
    public sealed record RegisterRequestDto(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
}
