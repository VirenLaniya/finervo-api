using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.Commands.ResendVerification
{
    internal sealed class ResendVerificationCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        : IRequestHandler<ResendVerificationCommand, Result>
    {
        public async Task<Result> Handle(
            ResendVerificationCommand command,
            CancellationToken ct)
        {
            var user = await userRepository.GetUserByEmailAsync(command.Email, ct);

            // Return success even if user not found — prevent email enumeration
            if (user is null)
                return Result.Success();

            // Already verified — return success silently
            if (user.IsEmailVerified)
                return Result.Success();

            var result = user.ResendVerification();
            if (!result.IsSuccess)
                return result;

            userRepository.Update(user);
            await unitOfWork.SaveChangesAsync(ct);

            return Result.Success();
        }
    }
}
