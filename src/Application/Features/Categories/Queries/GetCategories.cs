using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Application.Entities;
using MinimalApiArchitecture.Application.Infrastructure.Persistence;

namespace MinimalApiArchitecture.Application.Features.Categories.Queries;

public class GetCategories
{
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