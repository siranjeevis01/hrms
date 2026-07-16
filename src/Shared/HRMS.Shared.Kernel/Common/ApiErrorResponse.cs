namespace HRMS.Shared.Kernel.Common;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Details { get; set; }
    public IReadOnlyList<ApiValidationError>? ValidationErrors { get; set; }
    public string? TraceId { get; set; }
    public string? CorrelationId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiErrorResponse Create(int statusCode, string message, string? details = null)
    {
        return new ApiErrorResponse
        {
            StatusCode = statusCode,
            Message = message,
            Details = details
        };
    }

    public static ApiErrorResponse CreateWithValidation(
        int statusCode,
        string message,
        IReadOnlyList<ApiValidationError> validationErrors)
    {
        return new ApiErrorResponse
        {
            StatusCode = statusCode,
            Message = message,
            ValidationErrors = validationErrors
        };
    }
}

public class ApiValidationError
{
    public string Field { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public object? AttemptedValue { get; set; }
}
