using System.Security.Cryptography;
using System.Text;

namespace HRMS.Services.Identity.Infrastructure.Services;

public interface ITotpService
{
    string GenerateSecret();
    string GetProvisioningUri(string secret, string email, string issuer);
    bool ValidateCode(string secret, string code, int toleranceWindows = 1);
    int GetRemainingSeconds();
}

public class TotpService : ITotpService
{
    private const string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
    private const int SecretLength = 32;
    private const int StepSeconds = 30;
    private const int CodeLength = 6;

    public string GenerateSecret()
    {
        var bytes = new byte[SecretLength];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        var sb = new StringBuilder(SecretLength);
        foreach (var b in bytes)
        {
            sb.Append(ValidChars[b % ValidChars.Length]);
        }
        return sb.ToString();
    }

    public string GetProvisioningUri(string secret, string email, string issuer)
    {
        var encodedIssuer = Uri.EscapeDataString(issuer);
        var encodedEmail = Uri.EscapeDataString(email);

        return $"otpauth://totp/{encodedIssuer}:{encodedEmail}?secret={secret}&issuer={encodedIssuer}&algorithm=SHA1&digits={CodeLength}&period={StepSeconds}";
    }

    public bool ValidateCode(string secret, string code, int toleranceWindows = 1)
    {
        if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(code))
            return false;

        var key = Base32Decode(secret);
        var totp = new OtpNet.Totp(key, CodeLength, OtpNet.OtpHashMode.Sha1, StepSeconds);

        long timeStepUsed;
        return totp.VerifyTotp(code.Trim(), out timeStepUsed, new OtpNet.VerificationWindow(toleranceWindows));
    }

    public int GetRemainingSeconds()
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return StepSeconds - (int)(now % StepSeconds);
    }

    private static byte[] Base32Decode(string input)
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        input = input.TrimEnd('=').ToUpperInvariant();
        int byteCount = input.Length * 5 / 8;
        var result = new byte[byteCount];
        int buffer = 0, bitsLeft = 0, index = 0;
        foreach (char c in input)
        {
            int val = alphabet.IndexOf(c);
            if (val < 0) continue;
            buffer = (buffer << 5) | val;
            bitsLeft += 5;
            if (bitsLeft >= 8)
            {
                bitsLeft -= 8;
                result[index++] = (byte)(buffer >> bitsLeft);
            }
        }
        return result;
    }
}
