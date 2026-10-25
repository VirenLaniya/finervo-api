using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.Commands.ResendVerification
{
    public sealed record ResendVerificationCommand(string Email) : IRequest<Result>;
}
