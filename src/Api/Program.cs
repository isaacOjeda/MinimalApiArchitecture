using MinimalApiArchitecture.Api;
using MinimalApiArchitecture.Api.Extensions;
using MinimalApiArchitecture.Application;
using MinimalApiArchitecture.Application.Common.Modules;
using MinimalApiArchitecture.Application.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilog();

builder.Services.AddWebApiConfig();
builder.Services.AddApplicationCore();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseCors(AppConstants.CorsPolicy);
app.UseStaticFiles();
app.MapSwagger();
app.MapEndpointModules();
app.Run();