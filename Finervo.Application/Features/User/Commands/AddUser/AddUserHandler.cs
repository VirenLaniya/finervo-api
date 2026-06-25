using Finervo.Core.Interfaces;
using Finervo.Core.Primitives;
using Finervo.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.AddUser
{
    public class AddUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<AddUserCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Finervo.Core.Entities.User newUser = new Finervo.Core.Entities.User
                {
                    FirstName = request.addUserRequestDto.FirstName,
                    LastName = request.addUserRequestDto.LastName,
                    Email = request.addUserRequestDto.Email,
                    UserName = request.addUserRequestDto.UserName,
                    Password = request.addUserRequestDto.Password,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow,
                };

                userRepository.Add(newUser);

                await unitOfWork.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new Error("User.UnableToSaveNewUser", ex.Message));
            }
        }
    }
}
