namespace HRMS.Shared.Kernel.Common;

public class Error
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public string Code { get; }
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static Error NotFound(string code, string message) => new(code, message);
    public static Error BadRequest(string code, string message) => new(code, message);
    public static Error Conflict(string code, string message) => new(code, message);
    public static Error Unauthorized(string code, string message) => new(code, message);
    public static Error Forbidden(string code, string message) => new(code, message);
    public static Error Validation(string code, string message) => new(code, message);
    public static Error Failure(string code, string message) => new(code, message);

    public override bool Equals(object? obj)
    {
        if (obj is not Error other)
            return false;

        return Code == other.Code && Message == other.Message;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public override string ToString()
    {
        return $"Error: [{Code}] {Message}";
    }

    public static bool operator ==(Error? left, Error? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Error? left, Error? right)
    {
        return !Equals(left, right);
    }
}
