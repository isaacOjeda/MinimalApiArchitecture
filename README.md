# Minimal API Vertical Slice Architecture

This project is an experiment trying to create a solution template with Minimal APIs and Carter.

# Technologies and patterns used

- API
  - [Minimal API with .NET 6](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0) and [Carter](https://github.com/CarterCommunity/Carter)
  - [Vertical Slice Architecture](https://jimmybogard.com/vertical-slice-architecture/)
  - CQRS with [MediatR](https://github.com/jbogard/MediatR)
  - [FluentValidation](https://fluentvalidation.net/)
  - [AutoMapper](https://automapper.org/)
  - [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
  - [Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

- Testing
  - [NUnit](https://nunit.org/)
  - [FluentAssertions](https://fluentassertions.com/)
  - [Respawn](https://github.com/jbogard/Respawn)

- Angular
  - HttpClient generated with NSwag Studio and OpenAPI definition
  - Simple CRUD and nothing more

- Blazor
  - HttpClient generated with NSwag Studio and OpenAPI definition
  - Simple CRUD and nothing more	


# Getting started

The easiest way to get started is using Visual Studio 2022 or with `dotnet run` installing the .NET 6 SDK.

# Database Migrations

To create a new migration with `dotnet-ef` you first locate to API folder:
```bash
dotnet ef migrations add <MigrationName> --project ..\Application\ -o Infrastructure\Persistence\Migrations
```


And to update de database:
```bash
dotnet ef database update
```


# Overview

🏗️

## API

🏗️

## Application 

🏗️

### Entities

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer (this layer it's shared between all features)

### Infrastructure

🏗️

### Features

🏗️

# Credits

Inspired by:

- [ContosoUniversityDotNetCore-Pages](https://github.com/jbogard/ContosoUniversityDotNetCore-Pages) by Jimmy Bogard
- [Carter](https://github.com/CarterCommunity/Carter) by Carter Community