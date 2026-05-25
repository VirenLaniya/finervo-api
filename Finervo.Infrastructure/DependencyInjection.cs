using Finervo.Core;
using Finervo.Core.Interfaces;
using Finervo.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Finervo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

            return services;
        }
    }
}
