using Finervo.Core.Primitives;
using Finervo.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Finervo.API.Shared.Extensions
{
    public static class ModelBindingExtenstions
    {
        public static IServiceCollection ConfigureModelBindingErrors(this IServiceCollection services)
        {
            services
                .Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(e => e.Value?.Errors.Count > 0)
                            .ToDictionary(
                                kvp => kvp.Key.ToCamelCase(),
                                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                                );

                        var response = ValidationErrorResult.WithErrors(errors);

                        return new BadRequestObjectResult(response);
                    };
                });

            return services;
        }
    }
}
