using MediatR;
using Microsoft.Extensions.Logging;
using MinimalApiArchitecture.Application.Domain.Events;

namespace MinimalApiArchitecture.Application.Features.Products.EventHandlers
{
    public class PriceChangedEventHandler : INotificationHandler<ProductUpdatePriceEvent>
    {
        private readonly ILogger<PriceChangedEventHandler> _logger;

        public PriceChangedEventHandler(ILogger<PriceChangedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductUpdatePriceEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Minimal APIs Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
