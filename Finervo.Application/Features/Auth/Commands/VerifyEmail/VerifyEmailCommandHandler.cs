using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.Commands.VerifyEmail
{
    internal sealed class VerifyEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        : IRequestHandler<VerifyEmailCommand, Result>
    {
        public async Task<Result> Handle(VerifyEmailCommand command, CancellationToken ct)
        {
            var user = await userRepository.GetByIdAsync(command.UserId, ct);

            if (user is null)
                return Result.Failure(UserErrors.NotFound);

            var result = user.VerifyEmail(command.Token);
            if (!result.IsSuccess)
                return result;

            userRepository.Update(user);
            await unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
