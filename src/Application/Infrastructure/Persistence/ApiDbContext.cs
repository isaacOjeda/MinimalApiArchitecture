using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Application.Entities;

namespace MinimalApiArchitecture.Application.Infrastructure.Persistence;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
    }
}
