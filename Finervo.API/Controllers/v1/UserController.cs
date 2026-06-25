using Asp.Versioning;
using Finervo.Application.Features.User.Commands.AddUser;
using Finervo.Application.Features.User.Commands.UpdateUser;
using Finervo.Application.Features.User.Queries.GetUsers;
using Finervo.Contracts.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finervo.API.Controllers.v1
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ControllerName("user")]
    public class UserController(ISender sender) : ControllerBase
    {
        [HttpGet("get-users")]
        [EndpointName("GetUsers")]       
        [EndpointSummary("Get all users")]     
        [EndpointDescription("Returns a list of all available users")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await sender.Send(new GetUsersQuery(), ct);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add-user")]
        [EndpointName("AddUser")]
        [EndpointSummary("Add new user.")]
        [EndpointDescription("Adds a new user with the provided details. Returns true if the operation was successful.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser(AddUserRequestDto addUserRequestDto, CancellationToken ct)
        {

            var command = new AddUserCommand(addUserRequestDto);

            var result = await sender.Send(command, ct);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update-user")]
        [EndpointName("UpdateUser")]
        [EndpointSummary("Updates user.")]
        [EndpointDescription("Updates the user details. Returns true if the operation was successful.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestDto updateUserRequestDto, CancellationToken ct)
        {

            var command = new UpdateUserCommand(updateUserRequestDto);

            var result = await sender.Send(command, ct);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete-user")]
        [EndpointName("DeleteUser")]
        [EndpointSummary("Deletes user.")]
        [EndpointDescription("Delete the User. Returns true if the operation was successful.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken ct)
        {

            var command = new DeleteUserCommand(id);

            var result = await sender.Send(command, ct);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
