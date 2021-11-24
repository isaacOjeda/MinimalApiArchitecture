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
            return mediator.Send(new Query());
        })
        .WithName(nameof(GetCategories))
        .WithTags(nameof(Category));
    }

    public class Query : IRequest<List<Response>>
    {

    }

    public class Handler : IRequestHandler<Query, List<Response>>
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public Handler(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<Response>> Handle(Query request, CancellationToken cancellationToken) =>
            _context.Categories.ProjectTo<Response>(_mapper.ConfigurationProvider).ToListAsync();
    }

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