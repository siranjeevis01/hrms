using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Application.DTOs;

public class EmployeeHistoryDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public EmployeeAction Action { get; set; }
    public DateTime ActionDate { get; set; }
    public string? PreviousValue { get; set; }
    public string? NewValue { get; set; }
    public string? Reason { get; set; }
    public Guid? ApprovedBy { get; set; }
}
