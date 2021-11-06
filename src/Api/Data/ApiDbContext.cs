using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Api.Entities;

namespace MinimalApiArchitecture.Api.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
}
