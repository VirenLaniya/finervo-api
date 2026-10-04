using Finervo.Contracts.Responses.Role;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.Role.Queries.GetRoles
{
    public class GetRolesQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetRolesQuery, Result<IEnumerable<RoleResponseDto>>>
    {
        public async Task<Result<IEnumerable<RoleResponseDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = await roleRepository.GetAllAsync(cancellationToken);

                List<RoleResponseDto> response = [.. roles.Select(r => 
                    new RoleResponseDto(
                        r.Id,
                        r.Name,
                        r.Description,
                        r.IsActive,
                        r.LastUpdatedAt
                    )
                )];

                return Result<IEnumerable<RoleResponseDto>>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<RoleResponseDto>>.Failure(new Error("Role.UnableToFetchRoles", ex.Message));
            }
        }
    }
}
