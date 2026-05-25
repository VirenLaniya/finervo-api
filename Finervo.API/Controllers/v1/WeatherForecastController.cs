using Asp.Versioning;
using Finervo.Application.Features.WeatherForecast.Commands.CreateWeatherForecast;
using Finervo.Application.Features.WeatherForecast.Queries.GetWeatherForecastList;
using Finervo.Contracts.Requests;
using Finervo.Contracts.Responses;
using Finervo.Core.Entities;
using Finervo.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finervo.API.Controllers.v1
{
    [ApiController]
    [ControllerName("weather-forecast")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherForecastController(ISender sender) : ControllerBase
    {
        [HttpGet("get-weather-forecasts")]
        [EndpointName("GetWeatherForecasts")]       // internal name
        [EndpointSummary("Get all weather forecasts")]     // shows in Swagger
        [EndpointDescription("Returns a list of all available weather forecasts")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await sender.Send(new GetWeatherForecastListQuery(), ct);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add-weather-summary")]
        [EndpointName("AddWeatherSummary")]
        [EndpointSummary("Add weather summary.")]
        [EndpointDescription("Adds a new weather summary with the provided details. Returns true if the operation was successful.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddWeatherSummary(AddWeatherSummaryRequestDto addWeatherSummaryRequestDto, CancellationToken ct)
        {

            var command = new AddWeatherSummaryCommand(addWeatherSummaryRequestDto.Summary);

            var result = await sender.Send(command, ct);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
