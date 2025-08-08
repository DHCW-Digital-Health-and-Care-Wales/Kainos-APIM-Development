# Pull Requests

These standards help streamline code reviews and maintain high code quality:

## Title & Description
- PR titles **must follow Conventional Commits** format:
  - Examples:
    - `feat: add support for FHIR OperationOutcome mapping`
    - `fix: correct null pointer in exception handler`
    - `docs: update README with API usage examples`
- Include a summary of the changes, the motivation, and any relevant context.
- Reference related issues or tickets (e.g., `Fixes #123`).

## Scope
- Keep PRs focused and small. Prefer multiple smaller PRs over one large one.
- Avoid mixing unrelated changes.

## Review Process
- Assign appropriate reviewers.
- Label the PR (e.g., `bug`, `feature`, `refactor`, `WIP`).
- Respond to review comments promptly and respectfully.

## Merge Strategy
- Always use squash merging for cleaner history unless otherwise agreed.
- Rebase if needed to resolve conflicts before merging.
