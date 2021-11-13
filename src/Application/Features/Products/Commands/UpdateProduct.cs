using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class UpdateProduct
{
    public class Command : IRequest<Results<NotFound, Ok>>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(r => r.ProductId).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Price).NotEmpty();
        }
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

            product.Name = request.Name!;
            product.Description = request.Description!;
            product.Price = request.Price;

            await _context.SaveChangesAsync();

            return Results.Extensions.Ok();
        }
    }
}