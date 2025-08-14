# Commit Messages

This guide outlines how to write commit messages for documentation changes using the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) specification.

---

## Format

```
docs(scope): short description
```

- **Type**: `docs` — used for documentation-only changes.
- **Scope** *(optional)*: Specifies the part of the documentation affected.
- **Description**: A brief summary of the change.

---

## Examples

```bash
docs: update README with setup instructions
docs(api): clarify response format for GET /users
docs(readme): fix broken link to contributing guide
docs(changelog): add entries for v2.0.0 release
docs!: remove deprecated documentation for legacy API
```

---

## Breaking Changes

To indicate a **breaking change**, add an exclamation mark (`!`) after the type or scope:

```bash
docs!: remove deprecated documentation for legacy API
docs(api)!: restructure endpoint documentation format
```

You should also include a clear explanation of the breaking change in the commit body.

---

## Best Practices

- Use **imperative language**: “update README” not “updated README”.
- Keep messages **concise and descriptive**.
- Include a scope when relevant to improve clarity.
- Split commits based on scope of change - keep changes to documentation, code, and tests separate.

---

## Related Types

| Type     | Use Case                            |
|----------|-------------------------------------|
| `docs`   | Documentation-only changes          |
| `chore`  | Maintenance tasks (e.g., formatting)|
| `feat`   | New features (may include docs)     |
| `fix`    | Bug fixes (may include docs)        |

---

## References

- [Conventional Commits v1.0.0](https://www.conventionalcommits.org/en/v1.0.0/)
