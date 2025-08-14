using DHCW.PD.Exceptions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using TimeoutException = DHCW.PD.Exceptions.TimeoutException;

namespace DHCW.PD.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
	private readonly FhirJsonSerializer _serializer;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
		_serializer = new FhirJsonSerializer();
    }

    public async System.Threading.Tasks.Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Handling exception {Type}. Cause: {Message}", ex.GetType(), ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private System.Threading.Tasks.Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
		int statusCode = GetStatusCode(exception);
        context.Response.ContentType = ContentType.JSON_CONTENT_HEADER;
        context.Response.StatusCode = statusCode;

		OperationOutcome.IssueType issueCode = GetIssueCode(exception);
        OperationOutcome operationOutcome = new OperationOutcome
        {
            Issue = new List<OperationOutcome.IssueComponent>
            {
                new OperationOutcome.IssueComponent
                {
                    Severity = OperationOutcome.IssueSeverity.Error,
                    Code = issueCode,
                    Details = new CodeableConcept
                    {
                        Text = exception.Message
                    },
                    Diagnostics = exception.Message
                }
            }
        };

        var result = _serializer.SerializeToString(operationOutcome);

		_logger.LogInformation("Returning error response with status code {StatusCode} and issue code {IssueCode}", statusCode, issueCode);

        return context.Response.WriteAsync(result);
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            TimeoutException => StatusCodes.Status408RequestTimeout,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static OperationOutcome.IssueType GetIssueCode(Exception exception)
    {
        return exception switch
        {
            BadRequestException => OperationOutcome.IssueType.Invalid,
            UnauthorizedException => OperationOutcome.IssueType.Security,
            ForbiddenException => OperationOutcome.IssueType.Forbidden,
            NotFoundException => OperationOutcome.IssueType.NotFound,
            TimeoutException => OperationOutcome.IssueType.Timeout,
            _ => OperationOutcome.IssueType.Exception
        };
    }
}
