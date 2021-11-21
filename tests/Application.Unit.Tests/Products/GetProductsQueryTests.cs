using AutoMapper;
using FluentAssertions;
using MinimalApiArchitecture.Application.Domain.Entities;
using MinimalApiArchitecture.Application.Features.Products.Queries;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Unit.Tests.Products;

public class GetProductsQueryTests : TestBase
{
    private IConfigurationProvider? _configurationProvider;

    [OneTimeSetUp]
    public void GetProductsSetup()
    {
        _configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<GetProducts.MappingProfile>();
        });
    }

    [Test]
    public async Task GetProductsTest_Empty()
    {
        // Arrenge
        var query = new GetProducts();

        // Act
        var response = await query.Handler(Context, _configurationProvider!);

        // Assert
        response.Result.Should().BeEmpty();
    }


    [Test]
    public async Task GetProductsTest()
    {
        // Arrenge
        var query = new GetProducts();
        var category = await AddAsync(new Category(0, "Category 01"));
        await AddAsync(new Product(0, "name 1", "description 1", 999, category.CategoryId));


        // Act
        var response = await query.Handler(Context, _configurationProvider!);

        // Assert
        response.Result.Should().NotBeEmpty();
        response.Result.First().CategoryName.Should().Be(category.Name);
    }
}
