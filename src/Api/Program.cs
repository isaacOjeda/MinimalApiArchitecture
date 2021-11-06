using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApiArchitecture.Api.Data;
using MinimalApiArchitecture.Api.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSwagger();
builder.Services.AddCarter();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

app.MapSwagger();
app.MapCarter();
app.Run();

