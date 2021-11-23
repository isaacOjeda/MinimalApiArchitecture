using AutoMapper;
using AutoMapper.QueryableExtensions;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Application.Domain.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Categories.Queries;

public class GetCategories : ICarterModule

{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/categories", Handler)
            .WithName(nameof(GetCategories))
            .WithTags(nameof(Category));
    }

    public static Task<List<Response>> Handler(ApiDbContext context, IConfigurationProvider configuration) =>
        context.Categories.ProjectTo<Response>(configuration).ToListAsync();



    public class Response
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Category, Response>();
    }
}