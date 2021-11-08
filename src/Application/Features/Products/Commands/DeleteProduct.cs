using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class DeleteProduct
{
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
