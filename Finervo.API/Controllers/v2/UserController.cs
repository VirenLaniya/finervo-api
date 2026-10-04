using Asp.Versioning;
using Finervo.API.Shared.Controllers;
using Finervo.Application.Common.Interfaces;
using Finervo.Application.Features.User.Queries.GetMyProfile;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Entities;
using Finervo.Core.Primitives;
using Finervo.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Finervo.API.Controllers.v2
{
    [ApiVersion("2.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ControllerName("users")]
    public class UserController(ISender sender, ICurrentUserService currentUserService) : ApiController(sender, currentUserService)
    {
        [HttpGet("me")]
        [EndpointName("GetMyProfileV2")]
        [EndpointSummary("Retrieves the current user's profile")]
        [EndpointDescription("Fetches detailed profile information for the currently authenticated user based on their access token.")]
        [ProducesResponseType(typeof(Result<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMyProfile(CancellationToken ct)
        {
            var result = await Sender.Send(new GetMyProfileQuery(CurrentUser.UserId!.Value), ct);

            return HandleResult(result);
        }
    }
}
