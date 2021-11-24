namespace MinimalApiArchitecture.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapSwagger(this WebApplication app)
    {
        app.UseOpenApi();
        app.UseSwaggerUi3(settings =>
        {
            settings.Path = "/api";
        });

        return app;
    }
}