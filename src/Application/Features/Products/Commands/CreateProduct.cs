using Carter;
using Carter.ModelBinding;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Domain.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class CreateProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products", async (HttpRequest req, IMediator mediator, CreateProductCommand command) =>
        {
            return await mediator.Send(command);
        })
        .WithName(nameof(CreateProduct))
        .WithTags(nameof(Product))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status201Created);
    }

    public class CreateProductCommand : IRequest<IResult>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateProductHandler(ApiDbContext context, IValidator<CreateProductCommand> validator)
        : IRequestHandler<CreateProductCommand, IResult>
    {
        public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.GetValidationProblems());
            }

            var newProduct = new Product(0, request.Name, request.Description, request.Price, request.CategoryId);

            context.Products.Add(newProduct);

            await context.SaveChangesAsync();

            return Results.Created($"api/products/{newProduct.ProductId}", null);
        }
    }

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Price).NotEmpty();
        }
    }
}