using Carter;
using Carter.ModelBinding;
using MediatR;
using MinimalApiArchitecture.Api.Extensions;
using MinimalApiArchitecture.Api.Features.Products.Commands;
using MinimalApiArchitecture.Api.Features.Products.Queries;

namespace MinimalApiArchitecture.Api.Features.Products;

public class ProductsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", GetProductsAsync)
            .WithName("GetProducts");

        app.MapPost("api/products", CreateProductAsync)
            .WithName("CreateProduct")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status202Accepted);

        app.MapPut("api/products", UpdateProductAsync)
            .WithName("UpdateProduct")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound);
    }

    Task<List<GetProducts.Response>> GetProductsAsync(IMediator mediator) =>
        mediator.Send(new GetProducts.Query());

    async Task<IResult> CreateProductAsync(CreateProduct.Command command, IMediator mediator, HttpRequest req)
    {
        var result = req.Validate(command);

        if (!result.IsValid)
        {
            return Results.ValidationProblem(result.ToValidationProblems());
        }

        return await mediator.Send(command);
    }

    async Task<IResult> UpdateProductAsync(UpdateProduct.Command command, IMediator mediator, HttpRequest request)
    {
        var result = request.Validate(command);

        if (!result.IsValid)
        {
            return Results.ValidationProblem(result.ToValidationProblems());
        }

        return await mediator.Send(command);
    }
}
