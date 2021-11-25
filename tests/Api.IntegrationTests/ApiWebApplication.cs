using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Api.IntegrationTests;

public class ApiWebApplication : WebApplicationFactory<Api>
{
    public const string TestConnectionString = "Server=(localdb)\\mssqllocaldb;Database=MinimalApiArchitecture_TestDb;Trusted_Connection=True;MultipleActiveResultSets=false";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped(sp =>
            {
                // Usamos una LocalDB para pruebas de integración
                return new DbContextOptionsBuilder<ApiDbContext>()
                .UseSqlServer(TestConnectionString)
                .UseApplicationServiceProvider(sp)
                .Options;
            });
        });

        return base.CreateHost(builder);
    }
}
