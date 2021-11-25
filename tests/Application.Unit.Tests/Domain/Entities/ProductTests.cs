using FluentAssertions;
using MinimalApiArchitecture.Application.Domain.Entities;
using MinimalApiArchitecture.Application.Features.Products.Commands;
using NUnit.Framework;

namespace Application.Unit.Tests.Domain.Entities
{
    public class ProductTests
    {
        [TestCase(1, 0)]
        [TestCase(999, 1)]
        public void ProductPriceChanged(double price, int eventsCount)
        {
            // Arrenge
            var product = new Product(1, "Name 1", "Description 1", 1, 1);
            var command = new UpdateProduct.UpdateProductCommand
            {
                CategoryId = 1,
                Description = "New description",
                Name = "New name",
                Price = price
            };

            // Act
            product.UpdateInfo(command);

            // Assert
            product.Price.Should().Be(price);
            product.DomainEvents.Count.Should().Be(eventsCount);
        }
    }
}
