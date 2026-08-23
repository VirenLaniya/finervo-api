using Finervo.API.Shared.Extensions;
using Finervo.API.Shared.Middlewares;
using Serilog;

Log.Logger = new Serilog.LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Finervo API");

    var builder = WebApplication.CreateBuilder(args);

    Log.Information("Running in {Environment} environment", builder.Environment.EnvironmentName);

    builder.AddLogging();   // Add Finervo Logging

    #region Services Configuration

    builder.Services.AddServices(builder.Configuration, builder.Environment);    // Add Finervo Services

    builder.Services.AddControllers();

    #endregion

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApiWithSwagger();    // Swagger Document
    }

    #region Middleware Pipeline

    app.UseCustomMiddlewares(); // Use Finervo Middlewares

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    #endregion

    app.Run();
} 
catch(Exception ex)
{
    Log.Fatal(ex, "Finervo API failed to start");
}
finally
{
    Log.CloseAndFlush();
}


