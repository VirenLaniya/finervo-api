using Finervo.Application.Common.Validators;
using FluentValidation;

namespace Finervo.Application.Features.User.Commands.UpdateUser
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User Id is required");

            RuleFor(x => x.FirstName)
                .MustBeValidName("First Name");

            RuleFor(x => x.LastName)
                .MustBeValidName("Last Name");

            RuleFor(x => x.Email)
                .MustBeValidEmail();

            RuleFor(x => x.UserName)
                .MustBeValidUserName();
        }
    }
}
