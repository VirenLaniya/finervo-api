using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Auth.Commands.ResendVerification
{
    public class ResendVerificationCommandValidator : AbstractValidator<ResendVerificationCommand>
    {
        public ResendVerificationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");
        }
    }
}
