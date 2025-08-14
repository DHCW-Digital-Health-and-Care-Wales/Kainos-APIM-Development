# Linting

This guide defines the linting approach for C# projects using .NET, with a focus on leveraging `dotnet format` for consistent code style and formatting.

---

## Linters used on the project

### `dotnet format`

A command-line tool that applies code style and formatting rules based on your project's `.editorconfig`.

---

## Setup

The format tool comes preinstalled as part of the dotnet package.

```bash
dotnet format
```

If not available natively on host OS, it can be invoked through docker run command.

```bash
docker run --rm -v $(pwd):/src -w /src mcr.microsoft.com/dotnet/sdk:9.0 dotnet format
```

---

## Usage in CI

Add `dotnet format` to your CI pipeline to ensure code is properly formatted before merging:

```bash
dotnet format --verify-no-changes
```

This will fail the build if formatting issues are detected.

---

## Configuration

Use `.editorconfig` to define your formatting rules. Example:

```ini
# .editorconfig
[*.cs]
indent_style = space
indent_size = 4
dotnet_sort_system_directives_first = true
csharp_new_line_before_open_brace = all
```

---

## Best Practices

- Run `dotnet format` before committing code. Should be done automatically through git hooks.
- Use `--verify-no-changes` in CI to enforce formatting.
- Keep `.editorconfig` versioned and consistent across the team.

---

## References

- [dotnet format documentation](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format)
