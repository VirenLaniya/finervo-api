using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Role.Commands.UpdateRole
{
    public sealed class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Role Id is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage($"Description is required")
                .MaximumLength(250).WithMessage($"Description cannot exceed 250 characters");
        }
    }
}
