namespace HRMS.Services.Identity.Application.DTOs;

public class AuthResponseDto
{
    [System.Text.Json.Serialization.JsonPropertyName("token")]
    public string AccessToken { get; set; } = string.Empty;
    [System.Text.Json.Serialization.JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public UserDto User { get; set; } = null!;
}
