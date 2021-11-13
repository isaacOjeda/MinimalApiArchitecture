using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class DeleteProduct
{
    public class Command : IRequest<Results<NotFound, Ok>>
    {
        [FromRoute]
        public int ProductId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Results<NotFound, Ok>>
    {
        private readonly ApiDbContext _context;

        public Handler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Results<NotFound, Ok>> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.ProductId);

            if (product is null)
            {
                return Results.Extensions.NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Results.Extensions.Ok();
        }
    }
}