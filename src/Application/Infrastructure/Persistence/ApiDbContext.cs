using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using MinimalApiArchitecture.Application.Common.Interfaces;
using MinimalApiArchitecture.Application.Domain;
using MinimalApiArchitecture.Application.Domain.Entities;

namespace MinimalApiArchitecture.Application.Infrastructure.Persistence;

public class ApiDbContext : DbContext
{
    private readonly IDomainEventService _domainEventService;
    private readonly ILogger<ApiDbContext> _logger;
    private IDbContextTransaction _currentTransaction;

    public ApiDbContext(DbContextOptions<ApiDbContext> options, IDomainEventService domainEventService, ILogger<ApiDbContext> logger) : base(options)
    {

        _domainEventService = domainEventService;
        _logger = logger;

        _logger.LogDebug("DbContext created.");
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    public async Task BeginTransactionAsync()
    {
        _logger.LogDebug("Starting Transaction");

        _currentTransaction = await Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        _logger.LogDebug("Commiting Transaction");

        await _currentTransaction.CommitAsync();
    }

    public async Task RollbackTransaction()
    {
        _logger.LogDebug("Rolling back Transaction");

        await _currentTransaction.RollbackAsync();

        _currentTransaction.Dispose();
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}