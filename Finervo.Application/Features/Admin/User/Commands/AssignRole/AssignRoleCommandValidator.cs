using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Admin.User.Commands.AssignRole
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User id is required");

            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage("Role id is required");

            RuleFor(x => x.AssignedBy)
                .NotEmpty()
                .WithMessage("Assigned by is required");

            RuleFor(x => x.AssignedBy)
                .NotEqual(x => x.UserId)
                .WithMessage("You cannot assign a role to yourself");
        }
    }
}
