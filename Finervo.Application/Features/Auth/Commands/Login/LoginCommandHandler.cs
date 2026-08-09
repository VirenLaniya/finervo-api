using Finervo.Application.Common.Interfaces;
using Finervo.Contracts.Responses.Auth;
using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Interfaces.Security;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, ITokenService tokenService) : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
    {
        public async Task<Result<LoginResponseDto>> Handle(LoginCommand command, CancellationToken ct)
        {
            try
            {
                // Check if email exists
                var user = await userRepository.GetUserByEmailAsync(command.Email, ct);

                if (user is null || !passwordHasher.Verify(command.Password, user.Password))
                    return Result<LoginResponseDto>.Failure(UserErrors.InvalidCredentials);

                var accessToken = tokenService.GenerateAccessToken(user);
                var refreshToken = tokenService.GenerateRefreshToken();

                user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));

                userRepository.Update(user);

                await unitOfWork.SaveChangesAsync(ct);

                return Result<LoginResponseDto>.Success(new LoginResponseDto(user.Id, user.Email, accessToken, refreshToken));
            }
            catch (Exception ex)
            {
                return Result<LoginResponseDto>.Failure(new Error("User.UnableToAuthenticate", ex.Message));
            }
        }
    }
}
