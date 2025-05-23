using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace MinimalApiArchitecture.Application.Common.Modules;

public static class EndpointModuleExtensions
{
    public static IServiceCollection AddEndpointModules(this IServiceCollection services)
    {
        var moduleTypes = Assembly.GetExecutingAssembly().DefinedTypes
            .Where(t => typeof(IEndpointModule).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

        foreach (var type in moduleTypes)
        {
            services.AddTransient(typeof(IEndpointModule), type);
        }

        return services;
    }

    public static IEndpointRouteBuilder MapEndpointModules(this IEndpointRouteBuilder app)
    {
        var modules = app.ServiceProvider.GetServices<IEndpointModule>();

        foreach (var module in modules)
        {
            module.AddRoutes(app);
        }

        return app;
    }
}
