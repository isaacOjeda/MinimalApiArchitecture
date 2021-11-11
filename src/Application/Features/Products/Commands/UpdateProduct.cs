using Carter;
using Carter.ModelBinding;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Extensions;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class UpdateProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/products", async (Command command, IMediator mediator, HttpRequest request) =>
        {
            var result = request.Validate(command);

            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToValidationProblems());
            }

            return await mediator.Send(command);
        })
        .WithName("UpdateProduct")
        .WithTags("Products")
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status202Accepted)
        .Produces(StatusCodes.Status404NotFound);
    }

    public class Command : IRequest<IResult>
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
                return Results.NotFound();
            }

            product.Name = request.Name!;
            product.Description = request.Description!;
            product.Price = request.Price;

            await _context.SaveChangesAsync();

            return Results.Accepted();
        }
    }
}