using HRMS.Services.Leave.Domain.Enums;
using MediatR;

namespace HRMS.Services.Leave.Application.Commands.CreateLeaveType;

public class CreateLeaveTypeCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Color { get; set; }
    public string? Icon { get; set; }
    public bool IsPaid { get; set; }
    public bool IsUnlimited { get; set; }
    public int DefaultBalanceDays { get; set; }
    public int MaxBalanceDays { get; set; }
    public int MaxCarryForwardDays { get; set; }
    public int MaxEncashmentDays { get; set; }
    public int? CarryForwardExpiryMonths { get; set; }
    public bool AllowEncashment { get; set; }
    public bool AllowCarryForward { get; set; }
    public bool AllowHalfDay { get; set; }
    public int MinDaysPerRequest { get; set; }
    public int MaxDaysPerRequest { get; set; }
    public int? MaxConsecutiveDays { get; set; }
    public bool RequireDocumentation { get; set; }
    public int? DocumentationDaysThreshold { get; set; }
    public string Gender { get; set; } = "All";
    public int? ApplicableAfterDays { get; set; }
    public string AccrualType { get; set; } = "Annual";
    public decimal AccrualRate { get; set; }
    public Guid? TenantId { get; set; }
}
