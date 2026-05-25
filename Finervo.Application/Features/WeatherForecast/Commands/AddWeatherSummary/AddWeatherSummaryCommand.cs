using Finervo.Contracts.Responses;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.WeatherForecast.Commands.CreateWeatherForecast
{
    public sealed record AddWeatherSummaryCommand(string Summary) : IRequest<Result<bool>>
    {

    }
}
