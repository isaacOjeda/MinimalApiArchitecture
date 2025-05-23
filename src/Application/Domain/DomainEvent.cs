﻿using MediatR;

namespace MinimalApiArchitecture.Application.Domain;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}

public abstract class DomainEvent : INotification
{
    protected DomainEvent()
    {
        DateOccurred = DateTimeOffset.UtcNow;
    }

    public bool IsPublished { get; set; }

    // Using DateTimeOffset to ensure timezone information is preserved
    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}
