using Asp.Versioning;
using Finervo.API.Shared.Controllers;
using Finervo.Application.Common.Interfaces;
using Finervo.Application.Features.Role.Commands.AddRole;
using Finervo.Application.Features.Role.Commands.DeleteRole;
using Finervo.Application.Features.Role.Commands.UpdateRole;
using Finervo.Application.Features.Role.Queries.GetRoles;
using Finervo.Contracts.Requests.Role;
using Finervo.Contracts.Responses.Role;
using Finervo.Core.Primitives;
using Finervo.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finervo.API.Controllers.v1
{
    [Authorize(Roles = $"{SystemRoles.SuperAdmin}, {SystemRoles.Admin}")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [ApiController]
    [ControllerName("roles")]
    public class RolesController(ISender sender, ICurrentUserService currentUserService) : ApiController(sender, currentUserService)
    {
        [HttpGet("")]
        [EndpointName("GetRoles")]
        [EndpointSummary("Retrieves all roles")]
        [EndpointDescription("Retrieves a list of all roles available to the current application.")]
        [ProducesResponseType(typeof(Result<IEnumerable<RoleResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRoles(CancellationToken ct)
        {
            var result = await Sender.Send(new GetRolesQuery(), ct);

            return HandleResult(result);
        }

        [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpPost("")]
        [EndpointName("AddRole")]
        [EndpointSummary("Creates a new role")]
        [EndpointDescription("Creates a new role with the provided details.")]
        [ProducesResponseType(typeof(Result<RoleResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddRole([FromBody] AddRoleRequestDto addRoleRequestDto, CancellationToken ct)
        {
            var command = new AddRoleCommand(addRoleRequestDto.Name, addRoleRequestDto.Description);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }

        [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpPut("{id:guid}")]
        [EndpointName("UpdateRole")]
        [EndpointSummary("Updates the role")]
        [EndpointDescription("Updates the details of an existing role identified by their unique ID.")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateRole([FromRoute] Guid id, [FromBody] UpdateRoleRequestDto updateUserRequestDto, CancellationToken ct)
        {
            var command = new UpdateRoleCommand(id, updateUserRequestDto.Description, updateUserRequestDto.IsActive);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }

        [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpDelete("{id:guid}")]
        [EndpointName("DeleteRole")]
        [EndpointSummary("Deletes the role")]
        [EndpointDescription("Deletes an existing role identified by their unique ID.")]
        [ProducesResponseType(typeof(Result<RoleDeleteResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid id, CancellationToken ct)
        {
            var command = new DeleteRoleCommand(id);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }
    }
}
