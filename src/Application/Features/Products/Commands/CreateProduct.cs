using Carter.ModelBinding;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Products.Commands;

public class CreateProduct
{
    public static async Task<Results<ValidationProblem, Created>> Handler(
        Command command,
        HttpRequest req,
        ApiDbContext context)
    {
        var result = req.Validate(command);

        if (!result.IsValid)
        {
            return Results.Extensions.ValidationProblem(result.GetValidationProblems());
        }

        var newProduct = new Product(0, command.Name, command.Description, command.Price, command.CategoryId);

        context.Products.Add(newProduct);

        await context.SaveChangesAsync();

        return Results.Extensions.Created($"api/products/{newProduct.ProductId}", newProduct);
    }

    public class Command
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
}