using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using NUnit.Framework;
using Respawn;
using Respawn.Graph;
using System.Threading.Tasks;

namespace MinimalApiArchitecture.Api.IntegrationTests;

public class TestBase
{
    protected ApiWebApplication Application;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        Application = new ApiWebApplication();

        using var scope = Application.Services.CreateScope();

        EnsureDatabase(scope);
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        Application.Dispose();
    }

    [SetUp]
    public async Task Setup()
    {
        await ResetState();
    }

    [TearDown]
    public void Down()
    {

    }

    protected async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using var scope = Application.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();

        return entity;
    }

    protected async Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = Application.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    private static void EnsureDatabase(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();

        context.Database.Migrate();
    }

    private static async Task ResetState()
    {
        var checkpoint = await Respawner.CreateAsync(ApiWebApplication.TestConnectionString, new RespawnerOptions
        {
            TablesToIgnore = new Table[]
            {
                "__EFMigrationsHistory"
            }
        });


        await checkpoint.ResetAsync(ApiWebApplication.TestConnectionString);
    }

}
