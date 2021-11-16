using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;
using MinimalApis.Extensions.Results;

namespace MinimalApiArchitecture.Application.Features.Products.Queries;

public class GetProducts
{
    public static async Task<Ok<List<Response>>> Handler(ApiDbContext context, IConfigurationProvider configuration) =>
        Results.Extensions.Ok(
            await context.Products
                .ProjectTo<Response>(configuration)
                .ToListAsync()
        );


    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Product, Response>()
            .ForMember(
                d => d.CategoryName,
                opt => opt.MapFrom(mf => mf.Category != null ? mf.Category.Name : string.Empty)
            );
    }

    public record Response(int ProductId, string Name, string Description, double Price, string CategoryName);
}