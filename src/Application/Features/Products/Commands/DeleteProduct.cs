using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class DeleteProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/products/{productId}", Handler)
            .WithName(nameof(DeleteProduct))
            .WithTags(nameof(Product));
    }

    public async Task<Results<NotFound, Ok>> Handler(int productId, ApiDbContext context)
    {
        var product = await context.Products.FindAsync(productId);

        if (product is null)
        {
            return Results.Extensions.NotFound();
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return Results.Extensions.Ok();
    }
}