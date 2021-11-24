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

namespace MinimalApiArchitecture.Application.Features.Categories.Queries;

public class GetCategories : ICarterModule

{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/categories", (IMediator mediator) =>
        {
            return mediator.Send(new GetCategoriesQuery());
        })
        .WithName(nameof(GetCategories))
        .WithTags(nameof(Category));
    }

    public class GetCategoriesQuery : IRequest<List<GetCategoriesResponse>>
    {

    }

    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<GetCategoriesResponse>>
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken) =>
            _context.Categories.ProjectTo<GetCategoriesResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public class GetCategoriesResponse
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
    }

    public class GetCategoriesMappingProfile : Profile
    {
        public GetCategoriesMappingProfile() => CreateMap<Category, GetCategoriesResponse>();
    }
}