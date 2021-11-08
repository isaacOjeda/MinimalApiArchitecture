using FluentAssertions;
using MinimalApiArchitecture.Application.Entities;
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
    public async Task GetProducts_Empty()
    {
        // Arrenge
        var client = Application.CreateClient();

        // Act
        var products = await client.GetFromJsonAsync<List<GetProducts.Response>>("/api/products");

        // Assert
        products.Should().BeEmpty();
    }

    [Test]
    public async Task GetProducts()
    {
        // Arrenge
        var testCategory = new Category(0, "Category Test");
        await AddAsync(testCategory);
        await AddAsync(new Product(0, "Test 01", "Desc 01", 1, testCategory.CategoryId));
        await AddAsync(new Product(0, "Test 02", "Desc 02", 2, testCategory.CategoryId));

        var client = Application.CreateClient();

        // Act
        var products = await client.GetFromJsonAsync<List<GetProducts.Response>>("/api/products");

        // Assert
        products.Should().NotBeNullOrEmpty();
        products.Count.Should().Be(2);
    }

    [Test]
    public async Task CreateProduct()
    {
        // Arrenge
        var testCategory = new Category(0, "Category Test");
        await AddAsync(testCategory);

        var client = Application.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("api/products", new CreateProduct.Command
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
        var testCategory = new Category(0, "Category Test");
        await AddAsync(testCategory);
        var product1 = new Product(0, "Test 01", "Desc 01", 1, testCategory.CategoryId);
        var product2 = new Product(0, "Test 02", "Desc 02", 2, testCategory.CategoryId);

        await AddAsync(product1);
        await AddAsync(product2);

        var client = Application.CreateClient();

        // Act
        var response = await client.PutAsJsonAsync("api/products", new UpdateProduct.Command
        {
            Description = "Updated Desc for ID 1",
            Name = "Updated name for ID 1",
            Price = 999,
            ProductId = product1.ProductId
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
        var testCategory = new Category(0, "Category Test");
        await AddAsync(testCategory);
        var product1 = new Product(0, "Test 01", "Desc 01", 1, testCategory.CategoryId);
        await AddAsync(product1);

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