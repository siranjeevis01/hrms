using MediatR;

namespace HRMS.Services.Notification.Application.Commands.SendSms;

public record SendSmsCommand : IRequest<Guid>
{
    public string PhoneNumber { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public string Provider { get; init; } = "Twilio";
    public Guid? TenantId { get; init; }
}
