using Finervo.Shared.Constants;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Finervo.API.Shared.OpenApi.Transformers
{
    public class CorrelationIdHeaderTransformer : IOpenApiOperationTransformer
    {
        public Task TransformAsync(
            OpenApiOperation operation,
            OpenApiOperationTransformerContext context,
            CancellationToken cancellationToken)
        {
            operation.Parameters ??= [];

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = CustomHeaders.CorrelationId,
                In = ParameterLocation.Header,
                Required = false,
                Description = "Optional correlation ID used to trace the request.",
                Schema = new OpenApiSchema
                {
                    Type = JsonSchemaType.String
                }
            });

            return Task.CompletedTask;
        }
    }
}
