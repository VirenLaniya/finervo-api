using Finervo.Application.Common.Validators;
using FluentValidation;

namespace Finervo.Application.Features.Auth.Commands.Register
{
    public sealed class LoginCommandValidator : AbstractValidator<RegisterCommand>
    {
        public LoginCommandValidator() 
        {
            RuleFor(x => x.FirstName)
                .MustBeValidName("First Name");

            RuleFor(x => x.LastName)
                .MustBeValidName("Last Name");

            RuleFor(x => x.Email)
                .MustBeValidEmail();

            RuleFor(x => x.Password)
                .MustBeValidPassword();

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Please confirm your password")
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
