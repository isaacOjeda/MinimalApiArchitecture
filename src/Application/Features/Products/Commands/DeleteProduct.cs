using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Domain.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class DeleteProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/products/{productId}", async (IMediator mediator, int productId) =>
        {
            return await mediator.Send(new DeleteProductCommand(productId));
        })
        .WithName(nameof(DeleteProduct))
        .WithTags(nameof(Product))
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }

    public class DeleteProductCommand : IRequest<IResult>
    {
        public DeleteProductCommand(int productId) => ProductId = productId;

        public int ProductId { get; set; }
    }

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, IResult>
    {
        private readonly ApiDbContext _context;

        public DeleteProductHandler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.ProductId);

            if (product is null)
            {
                return Results.NotFound();
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}