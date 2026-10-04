using Finervo.Contracts.Responses.User;
using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using Finervo.Shared.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Admin.User.Commands.AssignRole
{
    public class AssignRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<AssignRoleCommand, Result>
    {
        public async Task<Result> Handle(AssignRoleCommand command, CancellationToken ct)
        {
            try
            {
                #region Validations

                // Validate user exists
                var user = await userRepository.GetByIdWithRolesAsync(command.UserId, ct);

                if (user is null)
                    return Result<bool>.Failure(UserErrors.NotFound);

                // Validate role exists
                var role = await roleRepository.GetByIdAsync(command.RoleId, ct);

                if (role is null)
                    return Result<bool>.Failure(RoleErrors.NotFound);

                // Validate role is active
                if (!role.IsActive)
                    return Result.Failure(RoleErrors.RoleInactive);

                // Only Super admin can assign Super admin role
                if (role.Name == SystemRoles.SuperAdmin && !command.IsSuperAdmin)
                    return Result.Failure(RoleErrors.UnauthorizedSuperAdminAssignment);

                #endregion

                // Assign user role
                var assignResult = user.AssignRole(command.RoleId, command.AssignedBy);
                if (!assignResult.IsSuccess)
                    return Result<UserResponseDto>.Failure(assignResult.Error);

                userRepository.Update(user);

                await unitOfWork.SaveChangesAsync(ct);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result<UserResponseDto>.Failure(new Error("User.UnableToAssignRole", ex.Message));
            }
        }
    }
}
