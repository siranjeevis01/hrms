using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateEmployeePersonalInfo;

public class UpdateEmployeePersonalInfoCommand : IRequest<Unit>
{
    public Guid EmployeeId { get; set; }
    public string? PersonalEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public string? Nationality { get; set; }
    public string? BloodGroup { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
