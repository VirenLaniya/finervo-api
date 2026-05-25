using Finervo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Interfaces
{
    public interface IWeatherForecastRepository
    {
        IEnumerable<WeatherForecast> GetAll();

        void AddSummary(string summary);
    }
}
