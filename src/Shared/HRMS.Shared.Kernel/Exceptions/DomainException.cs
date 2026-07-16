namespace HRMS.Shared.Kernel.Exceptions;

public class DomainException : Exception
{
    public string Code { get; }
    public string? Details { get; }

    public DomainException(string code, string message)
        : base(message)
    {
        Code = code;
    }

    public DomainException(string code, string message, string? details)
        : base(message)
    {
        Code = code;
        Details = details;
    }

    public DomainException(string code, string message, Exception innerException)
        : base(message, innerException)
    {
        Code = code;
    }
}
