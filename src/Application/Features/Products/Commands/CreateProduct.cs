using Carter;
using Carter.ModelBinding;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Extensions;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class CreateProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products", async (Command command, IMediator mediator, HttpRequest req) =>
        {
            var result = req.Validate(command);

            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToValidationProblems());
            }

            return await mediator.Send(command);
        })
        .WithName("CreateProduct")
        .WithTags("Products")
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status202Accepted);
    }

    public class Command : IRequest<IResult>
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

    public class Handler : IRequestHandler<Command, IResult>
    {
        private readonly ApiDbContext _context;

        public Handler(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var newProduct = new Product(0, request.Name, request.Description, request.Price, request.CategoryId);

            _context.Products.Add(newProduct);

            await _context.SaveChangesAsync(cancellationToken);

            return Results.Accepted();
        }
    }
}
