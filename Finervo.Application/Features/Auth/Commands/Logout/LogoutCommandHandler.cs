using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand, Result>
    {
        public async Task<Result> Handle(LogoutCommand command, CancellationToken ct)
        {
            var user = await userRepository.GetByIdAsync(command.UserId, ct);
            if (user is null)
                return Result.Failure(UserErrors.NotFound);

            user.CleanRefreshToken();

            userRepository.Update(user);
            await unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
