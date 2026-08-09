using Finervo.API.Shared.Extensions;
using Finervo.API.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

#region Services Configuration

builder.Services.AddServices(builder.Configuration);    // Add Finervo Services

builder.Services.AddControllers();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApiWithSwagger();    // Swagger Document
}

#region Middleware Pipeline

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
