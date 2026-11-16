# Finervo Engineering Rules

## Before changing code

1. Read `docs/architecture/overview.md`.
2. Read `docs/architecture/dependency-rules.md`.
3. Check existing tests before adding new behavior.
4. Do not introduce a project dependency without updating the architecture documentation.
5. Add or update architecture tests when changing project references or layer boundaries.

## Architecture

### `Finervo.Shared`

- Contains reusable, foundational code only.
- Must not depend on `Finervo.Contracts`, `Finervo.Core`, `Finervo.Application`, `Finervo.Infrastructure`, or `Finervo.API`.
- Must not contain feature-specific business logic.

### `Finervo.Contracts`

- Contains API contracts, request models, response models, and shared contract types.
- Must not depend on `Finervo.Core`, `Finervo.Application`, `Finervo.Infrastructure`, or `Finervo.API`.
- Must remain independent from persistence, HTTP, and infrastructure implementation details.

### `Finervo.Core`

- Contains domain entities, value objects, domain errors, and business rules.
- May depend on `Finervo.Shared`.
- Must not depend on `Finervo.Contracts`, `Finervo.Application`, `Finervo.Infrastructure`, or `Finervo.API`.
- Must not depend on ASP.NET Core, Entity Framework Core, PostgreSQL, email, or other infrastructure frameworks.

### `Finervo.Application`

- Contains use cases, application orchestration, commands, queries, handlers, validators, and application abstractions.
- May depend on `Finervo.Core`, `Finervo.Contracts`, and `Finervo.Shared`.
- Must not depend on `Finervo.Infrastructure` or `Finervo.API`.
- Must not directly use persistence or external integration implementations.

### `Finervo.Infrastructure`

- Implements persistence, authentication, caching, email, background jobs, and external integrations.
- May depend on `Finervo.Application`, `Finervo.Core`, and `Finervo.Shared`.
- Must not depend on `Finervo.API`.
- Infrastructure implementations must be exposed to the application through abstractions where appropriate.

### `Finervo.API`

- Is the HTTP transport and composition layer.
- May depend on `Finervo.Application`, `Finervo.Contracts`, `Finervo.Infrastructure`, and `Finervo.Shared`.
- Contains controllers, middleware, authentication configuration, API versioning, dependency injection composition, and HTTP-specific concerns.
- Must not contain domain or application business rules.
- Controllers should delegate business operations to the application layer.

## Dependency direction

Dependencies should follow this direction:

- Do not introduce circular dependencies.
- Do not bypass the application layer from API controllers.
- Do not reference infrastructure implementations from core or application code.
- Do not add a project reference without documenting and testing the architectural decision.

## Testing

- Every new business rule requires a unit test.
- Architectural changes require architecture tests.
- New project dependency rules must be represented in `Finervo.ArchitectureTests`.
- API behavior requires integration or contract tests.
- Infrastructure integrations require integration tests where practical.
- Do not disable architecture tests to make a build pass.
- Architecture tests must validate both layer dependencies and important framework boundaries.

## Code quality

- Nullable reference types must remain enabled.
- Prefer asynchronous APIs for I/O.
- Do not commit secrets, passwords, JWT keys, or real connection strings.
- Keep configuration values outside source code.
- Use cancellation tokens for long-running or I/O-bound operations where appropriate.
- Run `dotnet build` and `dotnet test` before submitting changes.
