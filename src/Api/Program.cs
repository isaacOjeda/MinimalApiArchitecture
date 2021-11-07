using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Api.Data;
using MinimalApiArchitecture.Api.Extensions;


const string CorsPolicy = nameof(CorsPolicy);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy, builder =>
    {
        builder.AllowAnyOrigin();
    });
});
builder.Services.AddSwagger();
builder.Services.AddCarter();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

app.UseCors(CorsPolicy);
app.MapSwagger();
app.MapCarter();
app.Run();

