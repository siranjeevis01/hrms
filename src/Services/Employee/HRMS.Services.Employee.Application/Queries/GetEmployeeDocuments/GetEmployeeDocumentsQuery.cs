using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeDocuments;

public class GetEmployeeDocumentsQuery : IRequest<List<EmployeeDocumentDto>>
{
    public Guid EmployeeId { get; set; }
}
