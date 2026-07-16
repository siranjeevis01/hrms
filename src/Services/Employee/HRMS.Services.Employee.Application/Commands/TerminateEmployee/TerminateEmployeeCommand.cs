using MediatR;

namespace HRMS.Services.Employee.Application.Commands.TerminateEmployee;

public class TerminateEmployeeCommand : IRequest<Unit>
{
    public Guid EmployeeId { get; set; }
    public string TerminationType { get; set; } = string.Empty;
    public DateTime LastWorkingDate { get; set; }
    public string? Reason { get; set; }
    public int? NoticePeriodDays { get; set; }
    public decimal? FinalSettlement { get; set; }
    public Guid? ApprovedBy { get; set; }
}
