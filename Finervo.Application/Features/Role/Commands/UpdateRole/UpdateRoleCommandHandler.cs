using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateRoleCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateRoleCommand command, CancellationToken ct)
        {
            try
            {
                var role = await roleRepository.GetByIdAsync(command.Id, ct);

                #region Validations

                // Role not exists
                if (role is null)
                    return Result<bool>.Failure(RoleErrors.NotFound);

                // Check for existing role with same name
                if (await roleRepository.ExistsByNameAsync(role.Name, role.Id, ct: ct))
                    return Result<bool>.Failure(RoleErrors.AlreadyExists);

                #endregion

                // Update description
                role.UpdateDescription(command.Description);

                // Set Inactive role
                if (!command.IsActive)
                {
                    var roleDeactivateResult = role.Deactivate();

                    if (!roleDeactivateResult.IsSuccess)
                        return Result<bool>.Failure(roleDeactivateResult.Error);
                }

                roleRepository.Update(role);

                await unitOfWork.SaveChangesAsync(ct);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new Error("Role.UnableToUpdateRole", ex.Message));
            }
        }
    }
}
