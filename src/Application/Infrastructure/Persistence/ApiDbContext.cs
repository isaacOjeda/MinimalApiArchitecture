using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using MinimalApiArchitecture.Application.Domain;
using MinimalApiArchitecture.Application.Domain.Entities;

namespace MinimalApiArchitecture.Application.Infrastructure.Persistence;

public class ApiDbContext : DbContext
{
    private readonly IPublisher _publisher;
    private readonly ILogger<ApiDbContext> _logger;
    private IDbContextTransaction? _currentTransaction;

    public ApiDbContext(DbContextOptions<ApiDbContext> options, IPublisher publisher, ILogger<ApiDbContext> logger) : base(options)
    {
        _publisher = publisher;
        _logger = logger;

        _logger.LogDebug("DbContext created.");
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public async Task BeginTransactionAsync()
    {
        _logger.LogDebug("Starting Transaction");

        _currentTransaction = await Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_currentTransaction is null)
        {
            return;
        }

        _logger.LogDebug("Commiting Transaction");

        await _currentTransaction.CommitAsync();
    }

    public async Task RollbackTransaction()
    {
        if (_currentTransaction is null)
        {
            return;
        }

        _logger.LogDebug("Rolling back Transaction");

        await _currentTransaction.RollbackAsync();

        _currentTransaction.Dispose();
        _currentTransaction = null;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        foreach (var @event in events)
        {
            @event.IsPublished = true;

            _logger.LogInformation("New domain event {Event}", @event.GetType().Name);

            // Note: If an unhandled exception occurs, all the saved changes will be rolled back
            // by the TransactionBehavior. Changing entity state and their events should be atomic
            await _publisher.Publish(@event);
        }

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
    }
}