using MediatR;
using Microsoft.Extensions.Logging;
using MinimalApiArchitecture.Application.Common.Models;
using MinimalApiArchitecture.Application.Domain.Events;

namespace MinimalApiArchitecture.Application.Features.Products.EventHandlers
{
    public class PriceChangedEventHandler : INotificationHandler<DomainEventNotification<ProductUpdatePriceEvent>>
    {
        private readonly ILogger<PriceChangedEventHandler> _logger;

        public PriceChangedEventHandler(ILogger<PriceChangedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProductUpdatePriceEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogWarning("Minimal APIs Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
