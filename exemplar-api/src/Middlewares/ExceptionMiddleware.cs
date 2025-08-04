using System.Text.Json;
using DHCW.PD.Exceptions;

namespace DHCW.PD.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
		RequestDelegate next, 
		ILogger<ExceptionMiddleware> logger
	)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Handling exception {ex.GetType()}. Cause: {ex.Message}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
		context.Response.ContentType = "application/fhir+json";
        context.Response.StatusCode = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            DHCW.PD.Exceptions.TimeoutException => StatusCodes.Status408RequestTimeout,
            _ => StatusCodes.Status500InternalServerError
        };

        var result = JsonSerializer.Serialize(new
        {
            error = exception.Message,
            statusCode = context.Response.StatusCode
        });

        return context.Response.WriteAsync(result);
    }
}
