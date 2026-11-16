# ADR-001: Use Layered Architecture

- Status: Accepted
- Date: 2026-11-15
- Decision owners: Finervo maintainers

## Context

Finervo contains HTTP handling, business rules, persistence, and external
integrations. Without defined boundaries, these responsibilities can become
coupled and difficult to test.

## Decision

Finervo uses a layered clean architecture consisting of:

- `Finervo.API`
- `Finervo.Application`
- `Finervo.Contracts`
- `Finervo.Core`
- `Finervo.Infrastructure`
- `Finervo.Shared`

Dependencies must follow the rules in
`docs/architecture/dependency-rules.md`.

## Consequences

### Positive

- Domain behavior is easier to test.
- Infrastructure can be replaced more easily.
- Architectural violations can be detected automatically.
- Responsibilities are clear.

### Negative

- Some types require additional abstractions.
- Small features may require changes in multiple projects.
- Mapping between contracts and domain models may be necessary.

## Alternatives considered

- A single-project architecture.
- A feature-only vertical slice architecture.
- A fully event-driven architecture.

These alternatives were not selected for the current project scope.