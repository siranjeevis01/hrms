namespace HRMS.Services.Identity.Infrastructure.Services;

public class TotpServiceAdapter : HRMS.Services.Identity.Application.Interfaces.ITotpService
{
    private readonly TotpService _totpService;

    public TotpServiceAdapter(TotpService totpService)
    {
        _totpService = totpService;
    }

    public string GenerateSecret() => _totpService.GenerateSecret();

    public string GetProvisioningUri(string email, string secret, string issuer)
        => _totpService.GetProvisioningUri(secret, email, issuer);

    public bool ValidateCode(string secret, string code)
        => _totpService.ValidateCode(secret, code);
}
