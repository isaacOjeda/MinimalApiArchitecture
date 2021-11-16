using Carter.ModelBinding;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class UpdateProduct
{
    public static async Task<Results<NotFound, Ok, ValidationProblem>> Handler(
        Command command,
        ApiDbContext context,
        HttpRequest request)
    {
        var result = request.Validate(command);

        if (!result.IsValid)
        {
            return Results.Extensions.ValidationProblem(result.GetValidationProblems());
        }

        var product = await context.Products.FindAsync(command.ProductId);

        if (product is null)
        {
            return Results.Extensions.NotFound();
        }

        product.Name = command.Name!;
        product.Description = command.Description!;
        product.Price = command.Price;

        await context.SaveChangesAsync();

        return Results.Extensions.Ok();
    }

    public class Command
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
}