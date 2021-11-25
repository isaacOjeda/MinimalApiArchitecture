using MinimalApiArchitecture.Application.Domain.Events;
using MinimalApiArchitecture.Application.Features.Products.Commands;

namespace MinimalApiArchitecture.Application.Domain.Entities;

public class Product : IHasDomainEvent
{
    public Product(int productId, string name, string description, double price, int categoryId)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }

    public int ProductId { get; set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }
    public int CategoryId { get; private set; }
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