namespace HRMS.Services.Identity.Infrastructure.Services;

public class PasswordHasherAdapter : HRMS.Services.Identity.Application.Interfaces.IPasswordHasher
{
    private readonly PasswordHasher _passwordHasher;

    public PasswordHasherAdapter(PasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string HashPassword(string password) => _passwordHasher.HashPassword(password);

    public bool VerifyPassword(string hashedPassword, string providedPassword)
        => _passwordHasher.VerifyPassword(providedPassword, hashedPassword);
}
