using Finervo.Contracts.Responses;
using Finervo.Core.Interfaces;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Features.WeatherForecast.Commands.CreateWeatherForecast
{
    public class AddWeatherSummaryHandler(IWeatherForecastRepository weatherForecastRepository) : IRequestHandler<AddWeatherSummaryCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddWeatherSummaryCommand command, CancellationToken cancellationToken)
        {
            weatherForecastRepository.AddSummary(command.Summary);

            return Result<bool>.Success(true);
        }
    }
}
