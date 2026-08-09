using Finervo.Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.AddUser
{
    public sealed class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator() 
        {
            RuleFor(x => x.FirstName)
                .MustBeValidName("First Name");

            RuleFor(x => x.LastName)
                .MustBeValidName("Last Name");

            RuleFor(x => x.Email)
                .MustBeValidEmail();

            RuleFor(x => x.UserName)
                .MustBeValidUserName();

            RuleFor(x => x.Password)
                .MustBeValidPassword();

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Please confirm your password")
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
