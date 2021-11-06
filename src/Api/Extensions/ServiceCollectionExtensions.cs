using Microsoft.OpenApi.Models;

namespace MinimalApiArchitecture.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
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
}
