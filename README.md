# Role-playing Game - API

![Workflow: .NET](https://github.com/rodolfo-brandao/rpg-api/actions/workflows/dotnet-ci.yml/badge.svg) [![SonarCloud](https://github.com/rodolfo-brandao/rpg-api/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/rodolfo-brandao/rpg-api/actions/workflows/build.yml) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=rodolfo-brandao_rpg-api&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=rodolfo-brandao_rpg-api) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=rodolfo-brandao_rpg-api&metric=coverage)](https://sonarcloud.io/summary/new_code?id=rodolfo-brandao_rpg-api)

## About
This is a portfolio project that aims to provide a **REST API** built on **[.NET](https://dotnet.microsoft.com/)** to manage characters based on the classic RPG universe.

The intention of this project is not to present an API rich in business rules, but rather to expose a series of good practices, conventions and patterns that I am aware of.

## Setup
As this project was built using **.NET**, its [SDK and Runtime](https://dotnet.microsoft.com/en-us/download) are required to run this application. After that, just follow the steps below:

1. *Clone* this repository
```bash
$ git clone https://github.com/rodolfo-brandao/rpg-api
```
2. *Restore*, *build* and *test* the application
```bash
$ cd rpg-api && dotnet restore && dotnet build --no-restore && dotnet test --no-build --verbosity normal
```

3. *Run* locally
```bash
$ cd Rpg.Api && dotnet run -p Rpg.Api.csproj
```

## Features
- [x] .NET 6 LTS
- [x] Domain-Driven Design
- [x] Entity Framework
- [x] Fluent API
- [x] FluentValidation
- [x] Fluent Assertions
- [x] MediatR
- [x] Null Object
- [x] Repository
- [x] Unit of Work
- [x] SQLite
- [x] xUnit

## License
[MIT License](LICENSE)

## Note
Any corrections or suggestions you feel are valid, feel free to open a pull request. In this way, we can discuss and analyze, in the same way that it will be helping me in my professional growth.
