using FluentAssertions;
using MinimalApiArchitecture.Application.Domain.Entities;
using MinimalApiArchitecture.Application.Features.Products.Commands;
using MinimalApiArchitecture.Application.Features.Products.Queries;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MinimalApiArchitecture.Api.IntegrationTests.Features;


public class ProductsModuleTests : TestBase
{
    [Test]
    public async Task GetProducts()
    {
        // Arrenge
        var testCategory = await AddAsync(new Category(0, "Category Test"));
        await AddAsync(new Product(0, "Test 01", "Desc 01", 1, testCategory.CategoryId));
        await AddAsync(new Product(0, "Test 02", "Desc 02", 2, testCategory.CategoryId));

        var client = Application.CreateClient();

        // Act
        var products = await client.GetFromJsonAsync<List<GetProducts.GetProductsResponse>>("/api/products");

        // Assert
        products.Should().NotBeNullOrEmpty();
        products.Count.Should().Be(2);
    }

    [Test]
    public async Task CreateProduct()
    {
        // Arrenge
        var testCategory = await AddAsync(new Category(0, "Category Test"));

        var client = Application.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("api/products", new CreateProduct.CreateProductCommand
        {
            Description = $"Test product description",
            Name = "Test name",
            Price = 123456,
            CategoryId = testCategory.CategoryId,
        });

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task UpdateProduct()
    {
        // Arrenge
        var testCategory = await AddAsync(new Category(0, "Category Test"));
        var product1 = await AddAsync(new Product(0, "Test 01", "Desc 01", 1, testCategory.CategoryId));
        await AddAsync(new Product(0, "Test 02", "Desc 02", 2, testCategory.CategoryId));

        var client = Application.CreateClient();

        // Act
        var response = await client.PutAsJsonAsync("api/products", new UpdateProduct.UpdateProductCommand
        {
            Description = "Updated Desc for ID 1",
            Name = "Updated name for ID 1",
            Price = 999,
            ProductId = product1.ProductId,
            CategoryId = product1.CategoryId
        });

        // Assert
        response.EnsureSuccessStatusCode();

        var updated = await FindAsync<Product>(product1.ProductId);

        updated.Name.Should().Be("Updated name for ID 1");
        updated.Description.Should().Be("Updated Desc for ID 1");
        updated.Price.Should().Be(999);
    }

    [Test]
    public async Task DeleteProduct()
    {
        // Arrenge
        var testCategory = await AddAsync(new Category(0, "Category Test"));
        var product1 = await AddAsync(new Product(0, "Test 01", "Desc 01", 1, testCategory.CategoryId));

        var client = Application.CreateClient();

        // Act
        var response = await client.DeleteAsync($"api/products/{product1.ProductId}");


        // Assert
        response.EnsureSuccessStatusCode();

        var deleted = await FindAsync<Product>(product1.ProductId);

        deleted.Should().BeNull();
    }

    [Test]
    public async Task DeleteProduct_Should_Fail()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var response = await client.DeleteAsync($"api/products/0");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}