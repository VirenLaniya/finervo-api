using Finervo.Contracts.Responses;
using Finervo.Core.Entities;
using Finervo.Core.Interfaces;
using Finervo.Core.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finervo.Application.Features.WeatherForecast.Queries.GetWeatherForecastList
{
    public class GetWeatherForecastListHandler(IWeatherForecastRepository weatherForecastRepository) : IRequestHandler<GetWeatherForecastListQuery, Result<IEnumerable<GetWeatherForecastListDto>>>
    {

        public async Task<Result<IEnumerable<GetWeatherForecastListDto>>> Handle(GetWeatherForecastListQuery request, CancellationToken cancellationToken)
        {
            var weatherForecastList = weatherForecastRepository.GetAll();

            List<GetWeatherForecastListDto> weatherForecastListDto = new();

            foreach(Core.Entities.WeatherForecast wf in weatherForecastList)
            {
                weatherForecastListDto.Add(new GetWeatherForecastListDto
                {
                    Date = wf.Date,
                    TemperatureC = wf.TemperatureC,
                    Summary = wf.Summary
                });
            }

            var result = Result<IEnumerable<GetWeatherForecastListDto>>.Success(weatherForecastListDto);

            return result;
        }
    }
}
