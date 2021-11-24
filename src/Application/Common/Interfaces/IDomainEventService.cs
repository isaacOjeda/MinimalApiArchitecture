using MinimalApiArchitecture.Application.Domain;

namespace MinimalApiArchitecture.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
