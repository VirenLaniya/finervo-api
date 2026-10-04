using Finervo.Core.Primitives;
using MediatR;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Security;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Errors;

namespace Finervo.Application.Features.Admin.User.Commands.AddUser
{
    public class AddUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork) : IRequestHandler<AddUserCommand, Result<UserResponseDto>>
    {
        public async Task<Result<UserResponseDto>> Handle(AddUserCommand command, CancellationToken ct)
        {
            try
            {
                // Check for existing user with same email
                if (await userRepository.ExistsByEmailAsync(command.Email, ct: ct))
                    return Result<UserResponseDto>.Failure(UserErrors.EmailAlreadyExists);

                // Check for existing user with same username
                if (await userRepository.ExistsByUsernameAsync(command.UserName, ct: ct))
                    return Result<UserResponseDto>.Failure(UserErrors.UsernameAlreadyExists);

                // Encrypt Password using hash
                string passwordHash = passwordHasher.Hash(command.Password);

                var newUserResult = Finervo.Core.Entities.User.Create(
                    firstName: command.FirstName,
                    lastName: command.LastName,
                    email: command.Email,
                    userName: command.UserName,
                    passwordHash: passwordHash
                );

                if (!newUserResult.IsSuccess)
                    return Result<UserResponseDto>.Failure(newUserResult.Error);

                var newUser = newUserResult.Data;

                // Assign user role
                var assignResult = newUser.AssignRole(command.RoleId, command.AssignedBy);
                if (!assignResult.IsSuccess)
                    return Result<UserResponseDto>.Failure(assignResult.Error);
                
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
    }
}
