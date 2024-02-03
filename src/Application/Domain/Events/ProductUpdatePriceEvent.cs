using MinimalApiArchitecture.Application.Domain.Entities;

namespace MinimalApiArchitecture.Application.Domain.Events
{
    public class ProductUpdatePriceEvent(Product product) : DomainEvent
    {
        public Product Product { get; set; } = product;
    }
}
