using Microsoft.AspNetCore.Http;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class DeleteProduct
{
    public static async Task<Results<NotFound, Ok>> Handler(int productId, ApiDbContext context)
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