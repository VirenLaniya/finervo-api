using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Role.Commands.AddRole
{
    public sealed class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage($"Role name is required")
                .MinimumLength(2).WithMessage($"Role name must be atleast 2 characters")
                .MaximumLength(50).WithMessage($"Role name cannot exceed 50 characters")
                .Matches("^[a-zA-Z0-9]+$").WithMessage($"Role name can only contain letters and digits. Spaces and special characters are not allowed.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage($"Description is required")
                .MaximumLength(250).WithMessage($"Description cannot exceed 250 characters");
        }
    }
}
