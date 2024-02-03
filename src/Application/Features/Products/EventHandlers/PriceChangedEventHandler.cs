using MediatR;
using Microsoft.Extensions.Logging;
using MinimalApiArchitecture.Application.Domain.Events;

namespace MinimalApiArchitecture.Application.Features.Products.EventHandlers
{
    public class PriceChangedEventHandler(ILogger<PriceChangedEventHandler> logger)
        : INotificationHandler<ProductUpdatePriceEvent>
    {
        public Task Handle(ProductUpdatePriceEvent notification, CancellationToken cancellationToken)
        {
            logger.LogWarning("Minimal APIs Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
