using FluentValidation;
using MediatR;
using MinimalApiArchitecture.Api.Data;
using MinimalApiArchitecture.Api.Entities;

namespace MinimalApiArchitecture.Api.Features.Products.Commands;

public class CreateProduct
{
    public class Command : IRequest<IResult>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
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

    public class Handler : IRequestHandler<Command, IResult>
    {
        private readonly ApiDbContext _context;

        public Handler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var newProduct = new Product(0, request.Name, request.Description, request.Price);

            _context.Products.Add(newProduct);

            await _context.SaveChangesAsync(cancellationToken);

            return Results.Accepted();
        }
    }
}
