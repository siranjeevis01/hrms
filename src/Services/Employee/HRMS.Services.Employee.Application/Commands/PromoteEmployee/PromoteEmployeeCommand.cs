using MediatR;

namespace HRMS.Services.Employee.Application.Commands.PromoteEmployee;

public class PromoteEmployeeCommand : IRequest<Unit>
{
    public Guid EmployeeId { get; set; }
    public Guid NewDesignationId { get; set; }
    public Guid? NewGradeId { get; set; }
    public decimal? NewSalary { get; set; }
    public DateTime EffectiveDate { get; set; }
    public string? Reason { get; set; }
    public Guid? ApprovedBy { get; set; }
}
