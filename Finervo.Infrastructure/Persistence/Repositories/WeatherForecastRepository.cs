using Finervo.Core.Entities;
using Finervo.Core.Interfaces;
using Finervo.Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Persistence.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        
        public IEnumerable<WeatherForecast> GetAll()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = TempStorage.Summaries[Random.Shared.Next(TempStorage.Summaries.Count)]
            })
            .ToArray();
        }

        public void AddSummary(string summary)
        {
            TempStorage.AddSummary(summary);
        }
    }
}
