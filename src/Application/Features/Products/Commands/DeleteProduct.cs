using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class DeleteProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/products/{productId}", (int productId, IMediator mediator) =>
            mediator.Send(new Command()
            {
                ProductId = productId
            }))
        .WithName("DeleteProduct")
        .WithTags("Products")
        .Produces(StatusCodes.Status202Accepted)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }

    public class Command : IRequest<IResult>
    {
        [FromRoute]
        public int ProductId { get; set; }
    }

    public class Handler : IRequestHandler<Command, IResult>
    {
        private readonly ApiDbContext _context;

        public Handler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.ProductId);

            if (product is null)
            {
                return Results.Problem(statusCode: StatusCodes.Status404NotFound);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Results.Accepted();
        }
    }
}