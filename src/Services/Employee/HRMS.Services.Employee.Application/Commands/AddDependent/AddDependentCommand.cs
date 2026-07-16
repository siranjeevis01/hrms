using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.AddDependent;

public class AddDependentCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public bool IsInsuranceBeneficiary { get; set; }
    public string? PhoneNumber { get; set; }
}
