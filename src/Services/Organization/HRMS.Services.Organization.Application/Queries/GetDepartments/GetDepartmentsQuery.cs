using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Queries.GetDepartments;

public record GetDepartmentsQuery : IRequest<List<DepartmentDto>>
{
    public Guid? CompanyId { get; init; }
    public Guid? BranchId { get; init; }
    public bool? IsActive { get; init; }
}
