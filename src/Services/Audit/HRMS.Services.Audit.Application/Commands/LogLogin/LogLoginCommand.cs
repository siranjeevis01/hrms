using MediatR;

namespace HRMS.Services.Audit.Application.Commands.LogLogin;

public class LogLoginCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Device { get; set; }
    public string? Browser { get; set; }
    public bool IsSuccessful { get; set; }
    public string? FailureReason { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
