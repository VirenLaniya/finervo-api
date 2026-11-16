# Contributing

## Before starting work

1. Read `AGENTS.md`.
2. Read the relevant architecture documentation.
3. Search for existing implementations and tests.
4. Identify the correct project for the change.

## Adding a feature

1. Define or update the contract in `Finervo.Contracts`.
2. Add domain behavior to `Finervo.Core` when applicable.
3. Add the use case to `Finervo.Application`.
4. Add infrastructure implementations only when required.
5. Add API endpoints in `Finervo.API`.
6. Add the appropriate tests.
7. Update documentation if the architecture or public behavior changed.

## Architecture changes

Architecture changes require:

1. An update to `dependency-rules.md`.
2. An architecture test update.
3. An ADR in `docs/architecture/decisions/`.
4. Review before merging.

## Pull request checklist

- [ ] The code builds successfully.
- [ ] Tests have been added or updated.
- [ ] Architecture tests pass.
- [ ] No secrets were committed.
- [ ] Documentation was updated when required.
- [ ] Database migrations are included when required.