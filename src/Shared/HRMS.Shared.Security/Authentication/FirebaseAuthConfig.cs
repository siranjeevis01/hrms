namespace HRMS.Shared.Security.Authentication;

public class FirebaseAuthConfig
{
    public string ProjectId { get; set; } = string.Empty;
    public string ServiceAccountKeyPath { get; set; } = string.Empty;
    public string ServiceAccountKeyJson { get; set; } = string.Empty;
    public bool EnableEmulator { get; set; }
    public string EmulatorHost { get; set; } = "localhost:9099";
}
