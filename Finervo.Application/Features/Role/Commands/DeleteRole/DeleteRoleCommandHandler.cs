using Finervo.Contracts.Responses.Role;
using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoleCommand, Result<RoleDeleteResponseDto>>
    {
        public async Task<Result<RoleDeleteResponseDto>> Handle(DeleteRoleCommand command, CancellationToken ct)
        {
            try
            {
                var role = await roleRepository.GetByIdAsync(command.Id, ct);

                #region Validations

                if (role is null)
                    return Result<RoleDeleteResponseDto>.Failure(RoleErrors.NotFound);

                if (role.IsSystem)
                    return Result<RoleDeleteResponseDto>.Failure(RoleErrors.CannotDeleteSystemRole);

                #endregion

                roleRepository.Delete(role);

                await unitOfWork.SaveChangesAsync(ct);

                RoleDeleteResponseDto response = new(role.Id, role.Name);

                return Result<RoleDeleteResponseDto>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<RoleDeleteResponseDto>.Failure(new Error("Role.UnableToDeleteRole", ex.Message));
            }
        }
    }
}
