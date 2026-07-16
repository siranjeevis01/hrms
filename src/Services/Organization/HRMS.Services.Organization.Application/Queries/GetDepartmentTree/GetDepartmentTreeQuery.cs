using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Queries.GetDepartmentTree;

public record GetDepartmentTreeQuery : IRequest<List<DepartmentDto>>
{
    public Guid CompanyId { get; init; }
}
