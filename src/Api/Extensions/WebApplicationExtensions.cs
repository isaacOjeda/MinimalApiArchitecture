namespace MinimalApiArchitecture.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            c.RoutePrefix = "api";
        });

        return app;
    }
}