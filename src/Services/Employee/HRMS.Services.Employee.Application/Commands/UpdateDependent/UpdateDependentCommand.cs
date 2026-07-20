using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateDependent;

public class UpdateDependentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Relationship { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public bool? IsInsuranceBeneficiary { get; set; }
    public string? PhoneNumber { get; set; }
}
