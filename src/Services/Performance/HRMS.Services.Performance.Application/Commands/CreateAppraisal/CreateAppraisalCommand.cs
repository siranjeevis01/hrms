using HRMS.Services.Performance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateAppraisal;

public class CreateAppraisalCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid? ManagerId { get; set; }
    public string Period { get; set; } = string.Empty;
    public AppraisalType Type { get; set; }
    public string? Comments { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
