using Carter;
using MediatR;
using MinimalApiArchitecture.Api.Extensions;
using MinimalApiArchitecture.Application;
using MinimalApiArchitecture.Application.Helpers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCustomCors();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddCarter();
builder.Services.AddAutoMapper(typeof(Application));
builder.Services.AddMediatR(typeof(Application));

var app = builder.Build();

app.UseCors(AppConstants.CorsPolicy);
app.MapSwagger();
app.MapCarter();
app.Run();