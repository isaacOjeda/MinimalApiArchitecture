using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.Unit.Tests;

public class TestBase
{
    public ApiDbContext Context { get; private set; }

    [SetUp]
    protected void Setup()
    {
        Context = DbContextInMemoryFactory.Create();
        Context.Database.EnsureCreated();
    }

    protected async Task<T> AddAsync<T>(T entity) where T : class
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    [TearDown]
    protected void TearDown()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
