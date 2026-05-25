using Finervo.Core.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Finervo.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<TempStorage>();

            return services;
        }
    }
}
