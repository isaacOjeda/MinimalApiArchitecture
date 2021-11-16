using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MinimalApiArchitecture.Application.Features.Categories.Queries;

namespace MinimalApiArchitecture.Application.Features.Categories;

public class CategoriesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/categories", GetCategories.Handler)
            .WithName(nameof(GetCategories))
            .WithTags(nameof(CategoriesModule));
    }
}
