using AutoMapper;
using FluentAssertions;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Features.Products.Queries;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Unit.Tests.Products;

public class GetProductsQueryTests : TestBase
{

    [Test]
    public async Task GetProductsTest_Empty()
    {
        // Arrenge
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<GetProducts.MappingProfile>();
        });
        var query = new GetProducts.Query();
        var handler = new GetProducts.Handler(Context, configurationProvider);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().BeEmpty();
    }


    [Test]
    public async Task GetProductsTest()
    {
        // Arrenge
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<GetProducts.MappingProfile>();
        });
        var query = new GetProducts.Query();
        var handler = new GetProducts.Handler(Context, configurationProvider);

        var category = await AddAsync(new Category(0, "Category 01"));
        var product = await AddAsync(new Product(0, "name 1", "description 1", 999, category.CategoryId));


        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().NotBeEmpty();
        response.First().CategoryName.Should().Be(category.Name);
    }
}
