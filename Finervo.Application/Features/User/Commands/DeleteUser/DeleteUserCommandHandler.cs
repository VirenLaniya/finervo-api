using Finervo.Contracts.Responses.User;
using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;

namespace Finervo.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Result<UserResponseDto>>
    {
        public async Task<Result<UserResponseDto>> Handle(DeleteUserCommand command, CancellationToken ct)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(command.Id, ct);

                if (user is null)
                    return Result<UserResponseDto>.Failure(UserErrors.NotFound);

                userRepository.Delete(user);

                await unitOfWork.SaveChangesAsync(ct);

                UserResponseDto userResponseDtos = new()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    CreatedAt = user.CreatedAt,
                    LastUpdatedAt = user.LastUpdatedAt,
                };

                return Result<UserResponseDto>.Success(userResponseDtos);
            }
            catch (Exception ex)
            {
                return Result<UserResponseDto>.Failure(new Error("User.UnableToDeleteUser", ex.Message));
            }
        }
    }
}
