using Finervo.Contracts.Responses.User;
using Finervo.Core.Entities;
using Finervo.Core.Interfaces;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.UpdateUser
{
    public class DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Result<UserResponseDto>>
    {
        public async Task<Result<UserResponseDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(request.Id);

                if (user is null)
                    return Result<UserResponseDto>.Failure(new Error("User.NotFound", "User not found."));

                userRepository.Delete(user);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                UserResponseDto userResponseDtos = new UserResponseDto
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
                return Result<UserResponseDto>.Failure(new Error("User.UnableToUpdateUser", ex.Message));
            }
        }
    }
}
