
## Dependency matrix

The following table defines the permitted project dependencies:

| Project | Allowed dependencies |
|---|---|
| `Finervo.Shared` | None |
| `Finervo.Contracts` | None |
| `Finervo.Core` | `Finervo.Shared` |
| `Finervo.Application` | `Finervo.Core`, `Finervo.Contracts`, `Finervo.Shared` |
| `Finervo.Infrastructure` | `Finervo.Application`, `Finervo.Core`, `Finervo.Shared` |
| `Finervo.API` | `Finervo.Application`, `Finervo.Contracts`, `Finervo.Infrastructure`, `Finervo.Shared` |

A project must not reference another project unless that dependency is listed
in the matrix.

## Forbidden dependencies

### `Finervo.Shared`

`Finervo.Shared` must not depend on:

- `Finervo.Contracts`
- `Finervo.Core`
- `Finervo.Application`
- `Finervo.Infrastructure`
- `Finervo.API`

It must not contain feature-specific business logic.

### `Finervo.Contracts`

`Finervo.Contracts` must not depend on:

- `Finervo.Core`
- `Finervo.Application`
- `Finervo.Infrastructure`
- `Finervo.API`

Contracts must remain independent from persistence, HTTP, and infrastructure
implementation details.

### `Finervo.Core`

`Finervo.Core` must not depend on:

- `Finervo.Contracts`
- `Finervo.Application`
- `Finervo.Infrastructure`
- `Finervo.API`

The core may depend on `Finervo.Shared` only when the shared type is genuinely
generic and has no application or infrastructure concern.

`Finervo.Core` must not depend on:

- ASP.NET Core
- Entity Framework Core
- PostgreSQL or Npgsql
- MailKit or SMTP implementations
- HTTP clients
- Database providers
- Infrastructure-specific frameworks

### `Finervo.Application`

`Finervo.Application` must not depend on:

- `Finervo.Infrastructure`
- `Finervo.API`

Application code must not directly use database contexts, database providers,
email clients, or other infrastructure implementations. Use abstractions and
application-facing interfaces instead.

### `Finervo.Infrastructure`

`Finervo.Infrastructure` must not depend on:

- `Finervo.API`

Infrastructure implements abstractions defined by the application or core
layers. The API layer composes infrastructure through dependency injection; the
infrastructure layer must not reference the API layer.

### `Finervo.API`

`Finervo.API` may depend on application contracts and infrastructure
composition, but it must not contain domain or application business rules.

API controllers should delegate business operations to
`Finervo.Application`.

## Project references versus type dependencies

Both of the following must follow the dependency matrix:

1. Direct `<ProjectReference>` entries in project files.
2. Type, namespace, and assembly dependencies in compiled code.

An unused `using` directive does not create a compiled dependency. Architecture
tests must therefore validate compiled dependencies, while project-reference
validation should verify direct project references separately where required.

## Architecture tests

Architectural restrictions must be enforced by tests in
`Finervo.ArchitectureTests`.

At minimum, tests should verify that:

- `Finervo.Shared` has no Finervo project dependencies.
- `Finervo.Contracts` has no domain, application, infrastructure, or API dependencies.
- `Finervo.Core` has no contracts, application, infrastructure, or API dependencies.
- `Finervo.Application` has no infrastructure or API dependencies.
- `Finervo.Infrastructure` has no API dependency.
- Core and application layers do not depend on persistence or external-service frameworks.
- Controllers exist only in `Finervo.API`.
- Entity Framework contexts exist only in `Finervo.Infrastructure`.

A failing architecture test must not be disabled or bypassed to make a build
pass.

## Exceptions

An exception to these rules requires:

1. An ADR in `docs/architecture/decisions/`.
2. An update to the relevant architecture test.
3. An explanation of why the dependency cannot be replaced with an abstraction.
4. Review from the project owner.

Temporary exceptions must include an expiration date or a follow-up task.

## Changes to the dependency matrix

When adding a new project or changing a project dependency:

1. Define the project's responsibility.
2. Update this document.
3. Update `AGENTS.md` if agent behavior is affected.
4. Add or update architecture tests.
5. Add an ADR when the change affects the overall architecture.
6. Run `dotnet build` and `dotnet test`.