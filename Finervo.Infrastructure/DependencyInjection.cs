using Finervo.Application.Common.Interfaces;
using Finervo.Core.Interfaces.Persistence;
using Finervo.Core.Interfaces.Repositories;
using Finervo.Core.Interfaces.Security;
using Finervo.Infrastructure.Authentication;
using Finervo.Infrastructure.Persistence;
using Finervo.Infrastructure.Persistence.Repositories;
using Finervo.Infrastructure.Persistence.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Finervo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services
                .AddDatabase(configuration)
                .AddAuthenticationServices(configuration)
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Postgres");

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                .UseNpgsql(connectionString, npgsql =>
                {
                    // Retry on transient failures
                    npgsql.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);

                    // Migration assembly
                    npgsql.MigrationsAssembly(
                        typeof(ApplicationDbContext).Assembly.FullName);
                })
                // Log SQL queries in development
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                );

            return services;
        }

        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<JwtOptions>().BindConfiguration(JwtOptions.SectionName).ValidateOnStart();

            var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero   // no grace period on expiry
                    };

                    // Register custom JWT events
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = CustomJwtBearerEvents.OnChallenge,
                        OnForbidden = CustomJwtBearerEvents.OnForbidden
                    };
                });

            services.AddAuthorization();
            services.AddHttpContextAccessor();  // Gets the access to the current HTTP request's HttpContext, By default it is available in controllers which inherits ControllerBase

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
