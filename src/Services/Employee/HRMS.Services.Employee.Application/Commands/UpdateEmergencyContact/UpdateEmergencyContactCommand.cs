using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateEmergencyContact;

public class UpdateEmergencyContactCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Relationship { get; set; }
    public string? PhoneNumber { get; set; }
    public string? SecondaryPhone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool? IsPrimary { get; set; }
}
