using Finervo.Application.Common.Interfaces;
using Finervo.Contracts.Responses.Role;
using Finervo.Core.Errors;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Interfaces.Security;
using Finervo.Core.Primitives;
using MediatR;
using System.Xml.Linq;

namespace Finervo.Application.Features.Role.Commands.AddRole
{
    public class AddRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRequestHandler<AddRoleCommand, Result<RoleResponseDto>>
    {
        public async Task<Result<RoleResponseDto>> Handle(AddRoleCommand command, CancellationToken ct)
        {
            try
            {
                // Check for existing role with same name
                if (await roleRepository.ExistsByNameAsync(command.Name, ct: ct))
                    return Result<RoleResponseDto>.Failure(RoleErrors.AlreadyExists);

                var newRoleResult = Core.Entities.Role.Create(
                    command.Name,
                    command.Description
                );

                if (!newRoleResult.IsSuccess)
                    return Result<RoleResponseDto>.Failure(newRoleResult.Error);

                var newRole = newRoleResult.Data;

                roleRepository.Add(newRoleResult.Data);

                await unitOfWork.SaveChangesAsync(ct);

                return Result<RoleResponseDto>.Success(new RoleResponseDto(
                    newRole.Id,
                    newRole.Name,
                    newRole.Description,
                    newRole.IsActive,
                    newRole.LastUpdatedAt
                    ));
            }
            catch (Exception ex)
            {
                return Result<RoleResponseDto>.Failure(new Error("Role.UnableToSaveNewRole", ex.Message));
            }
        }
    }
}
