using MinimalApiArchitecture.Application.Domain.Entities;

namespace MinimalApiArchitecture.Application.Domain.Events
{
    public class ProductUpdatePriceEvent : DomainEvent
    {
        public ProductUpdatePriceEvent(Product product)
        {
            Product = product;
        }

        public Product Product { get; set; }
    }
}
