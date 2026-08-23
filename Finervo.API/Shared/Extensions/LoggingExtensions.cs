using Finervo.Infrastructure.Configuration;
using Finervo.Shared.Constants;
using Serilog;
using System.Security.Claims;

namespace Finervo.API.Shared.Extensions
{
    public static class LoggingExtensions
    {
        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, configurations) =>
                {
                    configurations
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        //.Enrich.FromLogContext()  // Already configured in appsettings Enrich
                        //.Enrich.WithMachineName() // Already configured in appsettings Enrich
                        //.Enrich.WithThreadId()    // Already configured in appsettings Enrich
                        //.Enrich.WithProcessId()   // Already configured in appsettings Enrich
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);

                    var seqOptions = builder.Configuration.GetSection(SeqOptions.SectionName).Get<SeqOptions>();

                    if(seqOptions?.Enabled == true && !String.IsNullOrWhiteSpace(seqOptions.ServerUrl))
                    {
                        configurations.WriteTo.Seq(
                                serverUrl: seqOptions.ServerUrl,
                                apiKey: string.IsNullOrWhiteSpace(seqOptions.ApiKey) ? null : seqOptions.ApiKey
                            );
                    }
                }
            );

            return builder;
        }

        public static WebApplication UseRequestLogging(this WebApplication app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000}ms";

                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set(LogContextProperties.RequestHost,
                        httpContext.Request.Host.Value);

                    diagnosticContext.Set(LogContextProperties.RequestScheme,
                        httpContext.Request.Scheme);

                    diagnosticContext.Set(LogContextProperties.UserAgent,
                        httpContext.Request.Headers.UserAgent.ToString());

                    diagnosticContext.Set(LogContextProperties.UserId,
                        httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? "anonymous");

                    diagnosticContext.Set(LogContextProperties.CorrelationId,
                        httpContext.Request.Headers[CustomHeaders.CorrelationId]
                        .FirstOrDefault() ?? "none");
                };
            });

            return app;
        }
    }
}
