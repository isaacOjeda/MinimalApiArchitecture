using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Products.Queries;

public class GetProducts
{
    public class Query : IRequest<List<Response>>
    {

    }

    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Product, Response>()
            .ForMember(
                d => d.CategoryName,
                opt => opt.MapFrom(mf => mf.Category != null ? mf.Category.Name : string.Empty)
            );
    }

    public class Handler : IRequestHandler<Query, List<Response>>
    {
        private readonly ApiDbContext _context;
        private readonly AutoMapper.IConfigurationProvider _configuration;

        public Handler(ApiDbContext context, AutoMapper.IConfigurationProvider configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Task<List<Response>> Handle(Query request, CancellationToken cancellationToken) =>
            _context.Products
                .ProjectTo<Response>(_configuration)
                .ToListAsync();
    }

    public record Response(int ProductId, string Name, string Description, double Price, string CategoryName);
}
