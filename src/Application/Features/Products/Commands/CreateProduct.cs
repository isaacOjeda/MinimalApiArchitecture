using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class CreateProduct
{

    public class Command : IRequest<Created>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Price).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Created>
    {
        private readonly ApiDbContext _context;

        public Handler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Created> Handle(Command request, CancellationToken cancellationToken)
        {
            var newProduct = new Product(0, request.Name, request.Description, request.Price, request.CategoryId);

            _context.Products.Add(newProduct);

            await _context.SaveChangesAsync(cancellationToken);

            return Results.Extensions.Created($"api/products/{newProduct.ProductId}", newProduct);
        }
    }
}