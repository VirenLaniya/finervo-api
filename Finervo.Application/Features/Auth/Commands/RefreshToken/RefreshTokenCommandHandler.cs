using Finervo.Application.Common.Interfaces;
using Finervo.Contracts.Responses.Auth;
using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService) : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponseDto>>
    {
        public async Task<Result<RefreshTokenResponseDto>> Handle(RefreshTokenCommand command, CancellationToken ct)
        {
            try
            {
                // Check if user exists
                var user = await userRepository.GetByIdAsync(command.UserId, ct);

                if (user is null)
                    return Result<RefreshTokenResponseDto>.Failure(UserErrors.NotFound);

                // Validate refresh token exists + refresh token matches
                if (String.IsNullOrWhiteSpace(user.RefreshToken) || user.RefreshToken != command.RefreshToken)
                    return Result<RefreshTokenResponseDto>.Failure(UserErrors.InvalidRefreshToken);

                // Validate refresh token expiration
                if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    return Result<RefreshTokenResponseDto>.Failure(UserErrors.InvalidRefreshToken);

                var accessToken = tokenService.GenerateAccessToken(user);
                var refreshToken = tokenService.GenerateRefreshToken();

                user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));

                userRepository.Update(user);

                await unitOfWork.SaveChangesAsync(ct);

                return Result<RefreshTokenResponseDto>.Success(new RefreshTokenResponseDto(user.Id, accessToken, refreshToken));
            }
            catch (Exception ex)
            {
                return Result<RefreshTokenResponseDto>.Failure(new Error("User.UnableToAuthenticate", ex.Message));
            }
        }
    }
}
