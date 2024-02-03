using AutoMapper;
using AutoMapper.QueryableExtensions;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Application.Domain.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Products.Queries;

public class GetProducts : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", (IMediator mediator) =>
        {
            return mediator.Send(new GetProductsQuery());
        })
        .WithName(nameof(GetProducts))
        .WithTags(nameof(Product));

    }

    public class GetProductsQuery : IRequest<List<GetProductsResponse>>
    {

    }

    public class GetProductsHandler(ApiDbContext context, IMapper mapper)
        : IRequestHandler<GetProductsQuery, List<GetProductsResponse>>
    {
        public Task<List<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
            context.Products.ProjectTo<GetProductsResponse>(mapper.ConfigurationProvider).ToListAsync();
    }

    public class GetProductsMappingProfile : Profile
    {
        public GetProductsMappingProfile() => CreateMap<Product, GetProductsResponse>()
            .ForMember(
                d => d.CategoryName,
                opt => opt.MapFrom(mf => mf.Category != null ? mf.Category.Name : string.Empty)
            );
    }

    public record GetProductsResponse(int ProductId, string Name, string Description, double Price, string CategoryName);
}