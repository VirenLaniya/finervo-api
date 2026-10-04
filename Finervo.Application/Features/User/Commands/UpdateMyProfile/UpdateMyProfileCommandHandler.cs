using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.UpdateMyProfile
{
    public class UpdateMyProfileCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateMyProfileCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateMyProfileCommand command, CancellationToken ct)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(command.Id, ct);

                #region Validations
                // User not exists
                if (user is null)
                    return Result<bool>.Failure(UserErrors.NotFound);

                // Check for username availability
                if (await userRepository.ExistsByUsernameAsync(command.UserName, user.Id, ct))
                    return Result<bool>.Failure(UserErrors.UsernameAlreadyExists);

                // Check for email availability
                if (await userRepository.ExistsByEmailAsync(command.Email, user.Id, ct))
                    return Result<bool>.Failure(UserErrors.EmailAlreadyExists);
                #endregion

                var updateResult = user.UpdateProfile(
                    command.FirstName,
                    command.LastName,
                    command.Email,
                    command.UserName);

                userRepository.Update(user);

                await unitOfWork.SaveChangesAsync(ct);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new Error("User.UnableToUpdateUser", ex.Message));
            }
        }
    }
}
