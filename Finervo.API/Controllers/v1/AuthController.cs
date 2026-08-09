using Asp.Versioning;
using Finervo.API.Shared.Controllers;
using Finervo.Application.Common.Interfaces;
using Finervo.Application.Features.Auth.Commands.Login;
using Finervo.Application.Features.Auth.Commands.Logout;
using Finervo.Application.Features.Auth.Commands.RefreshToken;
using Finervo.Application.Features.Auth.Commands.Register;
using Finervo.Contracts.Requests.Auth;
using Finervo.Contracts.Responses.Auth;
using Finervo.Contracts.Responses.User;
using Finervo.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finervo.API.Controllers.v1
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ControllerName("auth")]
    public class AuthController(ISender sender, ICurrentUserService currentUserService) : ApiController(sender, currentUserService)
    {
        [HttpPost("register")]
        [EndpointName("Register")]
        [EndpointSummary("Create a new user account")]
        [EndpointDescription("Registers a new user. Returns user details with id if the operation was successful.")]
        // TO DO : Uncomment if verify email enabled -[EndpointDescription("Registers a new user and sends an email verification link. The account remains inactive until the email address is verified.")]
        [ProducesResponseType(typeof(Result<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto, CancellationToken ct)
        {
            var command = new RegisterCommand(registerRequestDto.FirstName, registerRequestDto.LastName, registerRequestDto.Email, registerRequestDto.Password, registerRequestDto.ConfirmPassword);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }

        [HttpPost("login")]
        [EndpointName("Login")]
        [EndpointSummary("Authenticate and obtain access tokens")]
        [EndpointDescription("Validates user credentials and returns a short-lived access token and a long-lived refresh token.")]
        // TO DO : Uncomment if verify email enabled - [EndpointDescription("Validates user credentials and returns a short-lived access token and a long-lived refresh token. Email must be verified before login is permitted.")]
        [ProducesResponseType(typeof(Result<LoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken ct)
        {
            var command = new LoginCommand(loginRequestDto.Email, loginRequestDto.Password);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }

        [HttpPost("refresh-token")]
        [EndpointName("RefreshToken")]
        [EndpointSummary("Reissue access token using a valid refresh token")]
        [EndpointDescription("Exchanges a valid refresh token for a new access token and refresh token pair. The previous refresh token is immediately invalidated upon successful exchange. If the refresh token is expired or invalid, the user must re-authenticate via the login endpoint.")]
        [ProducesResponseType(typeof(Result<RefreshTokenResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequestDto, CancellationToken ct)
        {
            var command = new RefreshTokenCommand(refreshTokenRequestDto.UserId, refreshTokenRequestDto.RefreshToken);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }

        [HttpPost("logout")]
        [Authorize]
        [EndpointName("Logout")]
        [EndpointSummary("Invalidate session and revoke refresh token")]
        [EndpointDescription("Clears the refresh token associated with the current session. The access token remains valid until its natural expiry.")]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            var command = new LogoutCommand(CurrentUser.UserId!.Value);

            var result = await Sender.Send(command, ct);

            return HandleResult(result);
        }
    }
}
