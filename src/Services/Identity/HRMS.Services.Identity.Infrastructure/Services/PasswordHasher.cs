using System.Text.RegularExpressions;

namespace HRMS.Services.Identity.Infrastructure.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    PasswordValidationResult ValidatePasswordStrength(string password);
}

public class PasswordValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();

    public static PasswordValidationResult Success() => new() { IsValid = true };
    public static PasswordValidationResult Failure(List<string> errors) => new() { IsValid = false, Errors = errors };
}

public class PasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 12;
    private const int MinLength = 8;
    private const int MaxLength = 128;

    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;
        if (string.IsNullOrWhiteSpace(hashedPassword))
            return false;

        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            return false;
        }
    }

    public PasswordValidationResult ValidatePasswordStrength(string password)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(password))
        {
            errors.Add("Password is required.");
            return PasswordValidationResult.Failure(errors);
        }

        if (password.Length < MinLength)
            errors.Add($"Password must be at least {MinLength} characters long.");

        if (password.Length > MaxLength)
            errors.Add($"Password must not exceed {MaxLength} characters.");

        if (!Regex.IsMatch(password, @"[A-Z]"))
            errors.Add("Password must contain at least one uppercase letter.");

        if (!Regex.IsMatch(password, @"[a-z]"))
            errors.Add("Password must contain at least one lowercase letter.");

        if (!Regex.IsMatch(password, @"\d"))
            errors.Add("Password must contain at least one digit.");

        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            errors.Add("Password must contain at least one special character.");

        if (Regex.IsMatch(password, @"(.)\1{2,}"))
            errors.Add("Password must not contain three or more consecutive identical characters.");

        return errors.Count == 0
            ? PasswordValidationResult.Success()
            : PasswordValidationResult.Failure(errors);
    }
}
