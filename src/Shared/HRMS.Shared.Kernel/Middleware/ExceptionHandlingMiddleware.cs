using System.Net;
using System.Text.Json;
using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HRMS.Shared.Kernel.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message, details) = exception switch
        {
            DomainException ex => (HttpStatusCode.BadRequest, ex.Message, ex.Details),
            BusinessRuleValidationException ex => (HttpStatusCode.BadRequest, ex.Message, string.Join("; ", ex.BrokenRules)),
            UnauthorizedAccessException ex => (HttpStatusCode.Unauthorized, "Unauthorized access", ex.Message),
            KeyNotFoundException ex => (HttpStatusCode.NotFound, "Resource not found", ex.Message),
            ArgumentException ex => (HttpStatusCode.BadRequest, "Invalid argument", ex.Message),
            InvalidOperationException ex => (HttpStatusCode.Conflict, "Operation conflict", ex.Message),
            TimeoutException => (HttpStatusCode.RequestTimeout, "Request timeout", "The operation timed out"),
            OperationCanceledException => (HttpStatusCode.ServiceUnavailable, "Operation cancelled", "The operation was cancelled"),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred", exception.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault()
            ?? context.TraceIdentifier;

        var errorResponse = ApiErrorResponse.Create(
            (int)statusCode,
            message,
            details);

        errorResponse.TraceId = context.TraceIdentifier;
        errorResponse.CorrelationId = correlationId;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, options));
    }
}
