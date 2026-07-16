namespace HRMS.Services.Identity.Application.Interfaces;

public interface ITotpService
{
    string GenerateSecret();
    string GetProvisioningUri(string email, string secret, string issuer);
    bool ValidateCode(string secret, string code);
}
