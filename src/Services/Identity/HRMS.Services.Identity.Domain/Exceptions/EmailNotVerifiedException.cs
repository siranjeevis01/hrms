namespace HRMS.Services.Identity.Domain.Exceptions;

public class EmailNotVerifiedException : Exception
{
    public EmailNotVerifiedException()
        : base("Email address has not been verified. Please check your inbox.") { }

    public EmailNotVerifiedException(string message)
        : base(message) { }

    public EmailNotVerifiedException(string message, Exception innerException)
        : base(message, innerException) { }
}
