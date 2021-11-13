using Carter;
using Carter.ModelBinding;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Features.Products.Commands;
using MinimalApiArchitecture.Application.Features.Products.Queries;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Products;

public class ProductsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", GetProducts);
        app.MapPost("api/products", CreateProduct);
        app.MapDelete("api/products/{productId}", DeleteProduct);
        app.MapPut("api/products", UpdateProduct);
    }

    public static async Task<Ok<List<GetProducts.Response>>> GetProducts(IMediator mediator)
    {
        return Results.Extensions.Ok(await mediator.Send(new GetProducts.Query()));
    }

    public static async Task<Results<ValidationProblem, Created>> CreateProduct(CreateProduct.Command command, IMediator mediator, HttpRequest req)
    {
        var result = req.Validate(command);

        if (!result.IsValid)
        {
            return Results.Extensions.ValidationProblem(result.GetValidationProblems());
        }

        return await mediator.Send(command);
    }

    public static async Task<Results<NotFound, Ok>> DeleteProduct(int productId, IMediator mediator)
    {
        return await mediator.Send(new DeleteProduct.Command()
        {
            ProductId = productId
        });
    }

    public static async Task<Results<NotFound, Ok, ValidationProblem>> UpdateProduct(UpdateProduct.Command command, IMediator mediator, HttpRequest request)
    {
        var result = request.Validate(command);

        if (!result.IsValid)
        {
            return Results.Extensions.ValidationProblem(result.GetValidationProblems());
        }

        return await mediator.Send(command);
    }
}