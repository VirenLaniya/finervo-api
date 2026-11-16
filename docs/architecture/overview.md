# Architecture Overview

## Architecture style

Finervo uses a layered architecture with the following flow:
```bash
HTTP request 
  ↓ 
Finervo.API 
  ↓ 
Finervo.Application 
  ↓ 
Finervo.Core
```
`Finervo.Infrastructure` provides implementations for persistence and external services.

## Project responsibilities

### `Finervo.API`

Responsible for:

- HTTP endpoints.
- Controllers.
- Middleware.
- Authentication and authorization configuration.
- API versioning.
- Dependency injection composition.
- Translating HTTP requests into application requests.

The API must not contain domain business rules.

### `Finervo.Application`

Responsible for:

- Use cases.
- Commands and queries.
- Handlers.
- Application services.
- Validation.
- Application-facing abstractions.

The application layer coordinates work but should not contain infrastructure
implementations.

### `Finervo.Contracts`

Responsible for:

- Request DTOs.
- Response DTOs.
- Public API contracts.
- Contract-specific enums.

Contracts should not depend on domain or infrastructure implementation details.

### `Finervo.Core`

Responsible for:

- Entities.
- Value objects.
- Domain errors.
- Domain services.
- Business rules and invariants.

The core must remain independent from HTTP, databases, and external services.

### `Finervo.Infrastructure`

Responsible for:

- Entity Framework Core.
- Database contexts and migrations.
- Repositories.
- Email providers.
- Authentication implementations.
- Caching.
- External integrations.
- Background jobs.

### `Finervo.Shared`

Responsible only for generic, reusable foundational code. It must not become
a container for feature-specific logic.

## Request flow

1. `Finervo.API` receives an HTTP request.
2. The API validates transport-level concerns.
3. The request is sent to `Finervo.Application`.
4. The application executes the use case.
5. The application uses abstractions implemented by `Finervo.Infrastructure`.
6. The API maps the result to an HTTP response.
