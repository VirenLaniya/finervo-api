using Finervo.Application.Common.Validators;
using FluentValidation;

namespace Finervo.Application.Features.Auth.Commands.Login
{
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        {
            RuleFor(x => x.Email)
                .MustBeValidEmail();

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
