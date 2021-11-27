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
  - Swagger with Code generation using [NSwag](https://github.com/RicoSuter/NSwag)
  - Logging with [Serilog](https://github.com/serilog/serilog-aspnetcore)
  - [Decorator](https://refactoring.guru/design-patterns/decorator) pattern using PipelineBehaviors

- Testing
  - [NUnit](https://nunit.org/)
  - [FluentAssertions](https://fluentassertions.com/)
  - [Respawn](https://github.com/jbogard/Respawn)

- Angular
  - HttpClient generated with NSwag and OpenAPI definition
  - Simple CRUD

- Blazor
  - HttpClient generated with NSwag and OpenAPI definition
  - Simple CRUD

# Common design principles

- Separation of concerns
- Encapsulation
- Explicit dependencies
- Single responsibility
- Persistence ignorance*


# Getting started

The easiest way to get started is using Visual Studio 2022 or installing the .NET 6 SDK with `dotnet run`.

# Database Migrations

To create a new migration with `dotnet-ef` you first need to locate your API folder and then write the following:
```bash
dotnet ef migrations add <MigrationName> --project ..\Application\ -o Infrastructure\Persistence\Migrations
```


Aldo, you need to update the database:
```bash
dotnet ef database update
```


# Overview

This project is an experiment trying to create a solution for Minimal APIs using Vertical Slice Architecture.

If you think this is highly coupled, the important thing is to minimize coupling between slices, and maximize coupling in a slice; 
if you need to change something (e.g. switching Entity Framework for Dapper), you only need to change the affected 
slices and not a big file with all of the data access.


If you want to learn more, the project is based on these resources:
- [Choosing between using clean or vertical](https://www.reddit.com/r/dotnet/comments/lw13r2/choosing_between_using_cleanonion_or_vertical/)
- [Restructuring to a Vertical Slice Architecture](https://codeopinion.com/restructuring-to-a-vertical-slice-architecture/#:~:text=With%20vertical%20slice%20architecture%2C%20you,size%20of%20the%20vertical%20slice.)
- [Vertical Slice Architecture - Jimmy Bogard](https://www.youtube.com/watch?v=SUiWfhAhgQw&feature=emb_logo&ab_channel=NDCConferences)

## API

Minimal API that only hosts the application and wires up all the dependencies

## Application

This project contains all the core and infrastructure of the application. The intention is to separate the application by functionality instead of technical concerns.

### Domain

This will contain all entities, enums, exceptions, interfaces, types, and logic specific to the domain layer (this layer is shared between all features).

We can have domain events, enterprise logic, value objects, etc. This layer (or folder in this project) has the same purpose according with DDD.

### Infrastructure

This layer contains classes for accessing external resources. These classes should be based on interfaces only if we need them for testing. For example, Entity Framework is testable, and repositories are not needed. 
But if external services are called, we should abstract these classes for easy testing.

### Features

This folder contains all the "slices" of functionality, and each slice does not overlap with other slices. If you need to change something, you only change a portion of 
a slice or, if new features are needed, you add code in new files which saves you from modifying large files (like repositories or services).


# Credits

Inspired by:

- [ContosoUniversityDotNetCore-Pages](https://github.com/jbogard/ContosoUniversityDotNetCore-Pages) by Jimmy Bogard
- [CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture) by Jason Taylor
- [Carter](https://github.com/CarterCommunity/Carter) by Carter Community