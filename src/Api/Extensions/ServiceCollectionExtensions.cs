using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalApiArchitecture.Application.Helpers;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsProvidesMetadataApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Description = "Minimal APIs & Angular",
                Title = "Minimal APIs & Angular",
                Version = "v1",
                Contact = new OpenApiContact()
                {
                    Name = "Isaac Ojeda",
                    Url = new Uri("https://github.com/isaacOjeda")
                }
            });
            c.CustomSchemaIds(x => x.FullName);
        });

        return services;
    }

    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: AppConstants.CorsPolicy, builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Default");

        services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}