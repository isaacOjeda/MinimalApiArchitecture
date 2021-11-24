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

    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<GetProductsResponse>>
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsHandler(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken) =>
            _context.Products.ProjectTo<GetProductsResponse>(_mapper.ConfigurationProvider).ToListAsync();
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