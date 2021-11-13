using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Features.Categories.Queries;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Categories;

public class CategoriesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/categories", GetCategories);
    }

    public static async Task<Ok<List<GetCategories.Response>>> GetCategories(IMediator mediator)
    {
        return Results.Extensions.Ok(await mediator.Send(new GetCategories.Query()));
    }
}
