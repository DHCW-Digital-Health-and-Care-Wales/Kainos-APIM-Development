# Logging

This guide outlines best practices for logging in .NET-based APIs, focusing on clarity, performance, and maintainability.

---

## Where to Log

| Layer        | Logging Recommendation                                  |
|--------------|----------------------------------------------------------|
| **Controllers** | Log incoming requests, key parameters, and response status. |
| **Services**    | Log business logic decisions, warnings, and exceptions.     |
| **Middlewares** | Log request/response lifecycle, authentication, and errors. |
| **Elsewhere**   | Can add additional logging if needed for debugging or tracing hard-to-find issues. |

---

## Logging Levels

| Level     | Use Case                                      |
|-----------|-----------------------------------------------|
| `Trace`   | Very detailed logs, typically only for debugging |
| `Debug`   | Useful for development and diagnostics         |
| `Information` | High-level application flow (e.g., startup, shutdown) |
| `Warning` | Unexpected but non-critical issues             |
| `Error`   | Recoverable failures or exceptions             |
| `Critical`| System-wide failures or crashes                |

---

## Example

```csharp
private readonly ILogger<MyService> _logger;

public MyService(ILogger<MyService> logger)
{
    _logger = logger;
}

public async Task ProcessOrderAsync(Order order)
{
    _logger.LogInformation("Processing order {OrderId}", order.Id);

    try
    {
        // Business logic
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Failed to process order {OrderId}", order.Id);
        throw;
    }
}
```

---

## CI & Observability

- Ensure logs are visible in your observability stack (e.g., Seq, ELK, Azure Monitor).
- Avoid logging sensitive data (e.g., passwords, tokens).
- Use correlation IDs for tracing across services.

---

## References

- [Microsoft Logging in .NET](https://learn.microsoft.com/en-us/dotnet/core/tools/)

