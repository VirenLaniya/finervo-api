using Asp.Versioning;
using Finervo.API.Shared.Controllers;
using Finervo.Application.Common.Interfaces;
using Finervo.Application.Features.Admin.User.Commands.AddUser;
using Finervo.Application.Features.Admin.User.Commands.AssignRole;
using Finervo.Application.Features.Admin.User.Commands.DeleteUser;
using Finervo.Application.Features.Admin.User.Commands.UpdateUser;
using Finervo.Application.Features.Admin.User.Queries.GetUser;
using Finervo.Application.Features.Admin.User.Queries.GetUsers;
using Finervo.Contracts.Requests.Admin.User;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using Finervo.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finervo.API.Controllers.v1
{
    [Authorize(Roles = $"{SystemRoles.SuperAdmin}, {SystemRoles.Admin}")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/admin/users")]
    [ApiController]
    [ControllerName("admin users")]
    public class AdminUserController(ISender sender, ICurrentUserService currentUserService) : ApiController(sender, currentUserService)
    {
        [HttpGet("")]
        [EndpointName("GetUsers")]
        [EndpointSummary("Retrieves all users")]
        [EndpointDescription("Retrieves a list of all users available to the current application.")]
        [ProducesResponseType(typeof(Result<IEnumerable<UserResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await Sender.Send(new GetUsersQuery(), ct);

            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        [EndpointName("GetUserById")]
        [EndpointSummary("Retrieves a user by ID")]
        [EndpointDescription("Retrieves the details of a specific user identified by their unique ID.")]
        [ProducesResponseType(typeof(Result<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await Sender.Send(new GetUserQuery(id), ct);

            return HandleResult(result);
        }

        [HttpPost("")]
        [EndpointName("AddUser")]
        [EndpointSummary("Creates a new user")]
        [EndpointDescription("Creates a new user with the provided details.")]
        [ProducesResponseType(typeof(Result<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDto addUserRequestDto, CancellationToken ct)
        {
            var command = AddUserCommand.FromRequest(addUserRequestDto, CurrentUser.UserId!.Value);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }

        [HttpPut("{id:guid}")]
        [EndpointName("UpdateUser")]
        [EndpointSummary("Updates a user")]
        [EndpointDescription("Updates the details of an existing user identified by their unique ID.")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequestDto updateUserRequestDto, CancellationToken ct)
        {
            var command = UpdateUserCommand.FromRequest(id, updateUserRequestDto);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }

        [HttpDelete("{id:guid}")]
        [EndpointName("DeleteUser")]
        [EndpointSummary("Deletes a user")]
        [EndpointDescription("Deletes an existing user identified by their unique ID.")]
        [ProducesResponseType(typeof(Result<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken ct)
        {
            var command = new DeleteUserCommand(id);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }

        [HttpPost("{userId:guid}/roles")]
        [EndpointName("AssignRole")]
        [EndpointSummary("Assigns a role to a user")]
        [EndpointDescription("Assigns an existing active role to the specified user. Only SuperAdmin can assign the SuperAdmin role.")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AssignRole([FromRoute] Guid userId, [FromBody] AssignRoleRequestDto assignRoleRequestDto, CancellationToken ct)
        {
            var command = new AssignRoleCommand(userId, assignRoleRequestDto.RoleId, CurrentUser.UserId!.Value, CurrentUser.IsSuperAdmin);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }
    }
}
