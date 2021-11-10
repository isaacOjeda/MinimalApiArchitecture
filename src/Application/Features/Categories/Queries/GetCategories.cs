using AutoMapper;
using AutoMapper.QueryableExtensions;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Categories.Queries;

public class GetCategories : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/categories", async (IMediator mediator) => await mediator.Send(new Query()))
            .WithName("GetCategories")
            .WithTags("Categories");
    }

    public class Query : IRequest<List<Response>>
    {

    }

    public class Handler : IRequestHandler<Query, List<Response>>
    {
        private readonly ApiDbContext _context;
        private readonly IConfigurationProvider _configuration;

        public Handler(ApiDbContext context, IConfigurationProvider configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public Task<List<Response>> Handle(Query request, CancellationToken cancellationToken) =>
            _context.Categories.ProjectTo<Response>(_configuration).ToListAsync();
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
