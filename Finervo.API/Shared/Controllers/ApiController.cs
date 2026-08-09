using Finervo.Application.Common.Interfaces;
using Finervo.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace Finervo.API.Shared.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class ApiController(ISender sender, ICurrentUserService currentUserService) : ControllerBase
    {
        protected readonly ISender Sender = sender;
        protected readonly ICurrentUserService CurrentUser = currentUserService;

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if(result is ValidationErrorResult<T> validationErrorResult)
            {
                return BadRequest(ValidationErrorResult<T>.WithErrors(
                        validationErrorResult.ValidationErrors
                    ));
            }

            if (result.IsSuccess)
                return Ok(result);

            return HandleErrorResult(result);
        }

        protected IActionResult HandleResult(Result result)
        {
            if (result is ValidationErrorResult validationErrorResult)
            {
                return BadRequest(ValidationErrorResult.WithErrors(
                        validationErrorResult.ValidationErrors
                    ));
            }

            if (result.IsSuccess)
                return Ok();

            return HandleErrorResult(result);
        }

        private IActionResult HandleErrorResult(Result result)
        {
            return result.Error.HttpStatuscode switch
            {
                HttpStatusCode.Unauthorized => Unauthorized(result),
                HttpStatusCode.NotFound => NotFound(result),
                HttpStatusCode.Conflict => Conflict(result),
                // everything else → 400
                _ => BadRequest(result)
            };
        }
    }
}
