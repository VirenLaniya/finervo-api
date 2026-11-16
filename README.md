# Finervo

Finervo is a financial management application built with ASP.NET Core and
React.

## Technology stack

- .NET 10
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- React
- Docker
- xUnit

## Backend projects

- `Finervo.API` - HTTP API and application composition.
- `Finervo.Application` - Use cases and application orchestration.
- `Finervo.Contracts` - API request and response contracts.
- `Finervo.Core` - Domain models and business rules.
- `Finervo.Infrastructure` - Persistence and external integrations.
- `Finervo.Shared` - Foundational shared code.

## Running the backend

1. Configure the required user secrets or environment variables.
2. Start PostgreSQL.
3. Run the API project from Visual Studio or the command line.
```bash
dotnet run --project Finervo.API
```

## Testing

```bash
dotnet test
```

## Architecture

Read:

- `docs/architecture/overview.md`
- `docs/architecture/dependency-rules.md`
- `docs/architecture/testing-strategy.md`
- `AGENTS.md`
