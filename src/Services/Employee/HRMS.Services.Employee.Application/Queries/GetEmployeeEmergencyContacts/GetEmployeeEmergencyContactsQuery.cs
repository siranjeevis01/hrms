using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeEmergencyContacts;

public class GetEmployeeEmergencyContactsQuery : IRequest<List<EmergencyContactDto>>
{
    public Guid EmployeeId { get; set; }
}
