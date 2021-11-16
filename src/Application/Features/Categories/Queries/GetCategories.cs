using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Categories.Queries;

public class GetCategories
{
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