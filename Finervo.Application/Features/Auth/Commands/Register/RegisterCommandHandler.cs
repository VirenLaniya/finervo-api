using Finervo.Contracts.Responses.User;
using Finervo.Core.Errors;
using Finervo.Core.Helpers;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Interfaces.Security;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork) : IRequestHandler<RegisterCommand, Result<UserResponseDto>>
    {
        public async Task<Result<UserResponseDto>> Handle(RegisterCommand command, CancellationToken ct)
        {
            try
            {
                // Check for existing user with same email
                var emailExists = await userRepository.ExistsByEmailAsync(command.Email, ct: ct);
                if (emailExists)
                    return Result<UserResponseDto>.Failure(UserErrors.EmailAlreadyExists);

                // Generate Unique Username
                string uniqueUsername = await GenerateUniqueUsernameAsync(command.FirstName, command.LastName, command.Email, ct);

                // Encrypt Password using hash
                string passwordHash = passwordHasher.Hash(command.Password);

                var newUserResult = Finervo.Core.Entities.User.Create(
                    firstName: command.FirstName,
                    lastName: command.LastName,
                    email: command.Email,
                    userName: uniqueUsername,
                    passwordHash: passwordHash
                );

                if (!newUserResult.IsSuccess)
                    return Result<UserResponseDto>.Failure(newUserResult.Error);

                var newUser = newUserResult.Data;

                //var accessToken = tokenService.GenerateAccessToken(newUser);
                //var refreshToken = tokenService.GenerateRefreshToken();

                //newUser.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));

                userRepository.Add(newUserResult.Data);

                await unitOfWork.SaveChangesAsync(ct);

                return Result<UserResponseDto>.Success(new()
                {
                    Id = newUser.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    UserName = newUser.UserName,
                    CreatedAt = newUser.CreatedAt,
                    LastUpdatedAt = newUser.LastUpdatedAt,
                });
            }
            catch (Exception ex)
            {
                return Result<UserResponseDto>.Failure(new Error("User.UnableToSaveNewUser", ex.Message));
            }
        }

        private async Task<string> GenerateUniqueUsernameAsync(string firstName, string lastName, string email, CancellationToken ct)
        {
            string userName = UsernameGenerator.Generate(firstName, lastName);
            int attempts = 0;
            
            while(await userRepository.ExistsByUsernameAsync(userName, ct: ct))
            {
                userName = attempts < 3 
                        ? UsernameGenerator.Generate(firstName, lastName) 
                        : UsernameGenerator.GenerateFromEmail(email);

                attempts++;
            }

            return userName;
        }
    }
}
