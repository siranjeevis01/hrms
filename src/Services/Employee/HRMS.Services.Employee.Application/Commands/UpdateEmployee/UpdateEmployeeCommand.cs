using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? DesignationId { get; set; }
    public Guid? GradeId { get; set; }
    public Guid? ReportsToId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? PreferredName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public EmploymentType? EmploymentType { get; set; }
    public DateTime? ConfirmationDate { get; set; }
}
