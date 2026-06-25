using Asp.Versioning;
using Finervo.Application.Features.User.Queries.GetUsers;
using Finervo.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finervo.API.Controllers.v2
{
    [ApiVersion("2.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ControllerName("user")]
    public class UserController(ISender sender) : ControllerBase
    {
        [HttpGet("get-users")]
        [EndpointName("GetUsersV2")]
        [EndpointSummary("Get all users")]
        [EndpointDescription("Returns a list of all available users")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await sender.Send(new GetUsersQuery(), ct);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
