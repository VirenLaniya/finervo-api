# Testing Strategy

## Test categories

### Unit tests

Unit tests verify isolated business behavior without databases, HTTP servers,
or external services.

Use unit tests for:

- Domain rules.
- Value objects.
- Error creation.
- Application handlers.
- Validators.
- Mappers.

### Architecture tests

Architecture tests verify project and namespace boundaries.

They must verify:

- `Finervo.Core` does not depend on infrastructure or API code.
- `Finervo.Application` does not depend on infrastructure or API code.
- `Finervo.Infrastructure` does not depend on API code.
- `Finervo.Shared` remains independent.
- `Finervo.Contracts` remains independent.
- Framework dependencies remain in the correct layers.

### Integration tests

Integration tests verify real infrastructure behavior.

Use them for:

- Entity Framework Core queries.
- PostgreSQL persistence.
- Authentication configuration.
- Email adapters.
- External service adapters.

### API tests

API tests verify the HTTP boundary.

Use them for:

- Status codes.
- Request validation.
- Authentication and authorization.
- Response contracts.
- API versioning.
- Middleware behavior.

## Test project responsibilities

| Test project | Responsibility |
|---|---|
| `Finervo.ArchitectureTests` | Layer and dependency rules |
| `Finervo.UnitTests` | Domain and application behavior |
| `Finervo.IntegrationTests` | Database and infrastructure behavior |
| `Finervo.ContractTests` | API request and response compatibility |
| `Finervo.ApiTests` | HTTP pipeline and endpoint behavior |

## Test naming

Use names that describe behavior:
```bash
MethodName_ShouldExpectedBehavior_WhenCondition
```
Example:
```bash
CreateTransaction_ShouldRejectNegativeAmount_WhenAmountIsInvalid
```

## Test requirements

- Every new business rule requires a unit test.
- Every new architectural boundary requires an architecture test.
- Every new endpoint requires API or contract tests.
- Infrastructure changes require integration tests where practical.
- Tests must not depend on execution order.
- Tests must not use production secrets or production data.

## Local verification
```bash
dotnet build dotnet test
```
