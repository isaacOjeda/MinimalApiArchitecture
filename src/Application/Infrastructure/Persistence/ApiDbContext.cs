using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Application.Entities;

namespace MinimalApiArchitecture.Application.Infrastructure.Persistence;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
    }
}
