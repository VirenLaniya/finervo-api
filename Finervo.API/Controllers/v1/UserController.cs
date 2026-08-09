using Asp.Versioning;
using Finervo.API.Shared.Controllers;
using Finervo.Application.Features.User.Commands.AddUser;
using Finervo.Application.Features.User.Commands.UpdateUser;
using Finervo.Application.Features.User.Commands.DeleteUser;
using Finervo.Application.Features.User.Queries.GetUsers;
using Finervo.Contracts.Requests.User;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Finervo.Application.Features.User.Queries.GetUser;
using Microsoft.AspNetCore.Authorization;
using Finervo.Application.Common.Interfaces;

namespace Finervo.API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ControllerName("user")]
    public class UserController(ISender sender, ICurrentUserService currentUserService) : ApiController(sender, currentUserService)
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
            var command = AddUserCommand.FromRequest(addUserRequestDto);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }

        [HttpPut("{id:guid}")]
        [EndpointName("UpdateUser")]
        [EndpointSummary("Updates a user")]
        [EndpointDescription("Updates the details of an existing user identified by their unique ID.")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
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
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken ct)
        {
            var command = new DeleteUserCommand(id);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }
    }
}
