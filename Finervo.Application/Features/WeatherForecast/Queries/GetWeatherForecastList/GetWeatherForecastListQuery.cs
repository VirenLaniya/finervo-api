using Finervo.Core.Primitives;
using Finervo.Contracts.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.WeatherForecast.Queries.GetWeatherForecastList
{
    public sealed record GetWeatherForecastListQuery : IRequest<Result<IEnumerable<GetWeatherForecastListDto>>>;
}
