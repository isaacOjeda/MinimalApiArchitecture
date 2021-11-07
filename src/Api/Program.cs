using Carter;
using MediatR;
using MinimalApiArchitecture.Api.Extensions;
using MinimalApiArchitecture.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCustomCors();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddCarter();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

app.UseCors(AppConstants.CorsPolicy);
app.MapSwagger();
app.MapCarter();
app.Run();

