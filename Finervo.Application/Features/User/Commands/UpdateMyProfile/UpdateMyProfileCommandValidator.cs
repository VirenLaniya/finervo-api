using Finervo.Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.User.Commands.UpdateMyProfile
{
    public sealed class UpdateMyProfileCommandValidator : AbstractValidator<UpdateMyProfileCommand>
    {
        public UpdateMyProfileCommandValidator()
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
