using Microsoft.AspNetCore.Routing;

namespace MinimalApiArchitecture.Application.Common.Modules;

public interface IEndpointModule
{
    void AddRoutes(IEndpointRouteBuilder app);
}
