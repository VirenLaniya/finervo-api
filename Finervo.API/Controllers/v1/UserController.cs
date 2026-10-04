using Asp.Versioning;
using Finervo.API.Shared.Controllers;
using Finervo.Application.Common.Interfaces;
using Finervo.Application.Features.User.Commands.UpdateMyProfile;
using Finervo.Application.Features.User.Queries.GetMyProfile;
using Finervo.Contracts.Requests.User;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finervo.API.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ControllerName("users")]
    public class UserController(ISender sender, ICurrentUserService currentUserService) : ApiController(sender, currentUserService)
    {
        [HttpGet("me")]
        [EndpointName("GetMyProfile")]
        [EndpointSummary("Retrieves the current user's profile")]
        [EndpointDescription("Fetches detailed profile information for the currently authenticated user based on their access token.")]
        [ProducesResponseType(typeof(Result<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMyProfile(CancellationToken ct)
        {
            var result = await Sender.Send(new GetMyProfileQuery(CurrentUser.UserId!.Value), ct);

            return HandleResult(result);
        }

        [HttpPut("me")]
        [EndpointName("UpdateMyProfile")]
        [EndpointSummary("Updates the current user's profile")]
        [EndpointDescription("Modifies the profile details of the currently authenticated user using the provided request data.")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateMyProfileRequestDto updateUserRequestDto, CancellationToken ct)
        {
            var command = UpdateMyProfileCommand.FromRequest(CurrentUser.UserId!.Value, updateUserRequestDto);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }
    }
}
