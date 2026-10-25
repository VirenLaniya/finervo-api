using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.Commands.VerifyEmail
{
    internal sealed class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User id is required");

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Verification token is required");
        }
    }
}
