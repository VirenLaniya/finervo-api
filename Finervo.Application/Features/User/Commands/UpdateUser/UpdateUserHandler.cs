using Finervo.Core.Interfaces;
using Finervo.Core.Primitives;
using Finervo.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(request.updateUserRequestDto.Id);

                if (user is null)
                    return Result<bool>.Failure(new Error("User.NotFound", "User not found."));

                var updateResult = user.UpdateProfile(
                    request.updateUserRequestDto.FirstName,
                    request.updateUserRequestDto.LastName,
                    request.updateUserRequestDto.Email,
                    request.updateUserRequestDto.UserName);

                userRepository.Update(user);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new Error("User.UnableToUpdateUser", ex.Message));
            }
        }
    }
}
