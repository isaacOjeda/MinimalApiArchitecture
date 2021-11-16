using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Features.Products.Commands;
using MinimalApiArchitecture.Application.Features.Products.Queries;

namespace MinimalApiArchitecture.Application.Features.Products;

public class ProductsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", GetProducts.Handler)
            .WithName(nameof(GetProducts))
            .WithTags(nameof(ProductsModule));
        app.MapPost("api/products", CreateProduct.Handler)
            .WithName(nameof(CreateProduct))
            .WithTags(nameof(ProductsModule));
        app.MapDelete("api/products/{productId}", DeleteProduct.Handler)
            .WithName(nameof(DeleteProduct))
            .WithTags(nameof(ProductsModule));
        app.MapPut("api/products", UpdateProduct.Handler)
            .WithName(nameof(UpdateProduct))
            .WithTags(nameof(ProductsModule));
    }
}