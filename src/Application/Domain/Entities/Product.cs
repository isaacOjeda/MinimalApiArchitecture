using MinimalApiArchitecture.Application.Domain.Events;
using MinimalApiArchitecture.Application.Features.Products.Commands;

namespace MinimalApiArchitecture.Application.Domain.Entities;

public class Product(int productId, string name, string description, double price, int categoryId)
    : IHasDomainEvent
{
    public int ProductId { get; set; } = productId;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public double Price { get; private set; } = price;
    public int CategoryId { get; private set; } = categoryId;
    public Category? Category { get; private set; }

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

    public void UpdateInfo(UpdateProduct.UpdateProductCommand command)
    {
        if (Price != command.Price)
        {
            DomainEvents.Add(new ProductUpdatePriceEvent(this));
        }

        Name = command.Name!;
        Description = command.Description!;
        Price = command.Price;
        CategoryId = command.CategoryId;
    }
}