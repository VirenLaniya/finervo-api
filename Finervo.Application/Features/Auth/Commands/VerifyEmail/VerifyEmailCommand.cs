using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.Commands.VerifyEmail
{
    public sealed record VerifyEmailCommand(
        Guid UserId,
        string Token
    ) : IRequest<Result>;
}
