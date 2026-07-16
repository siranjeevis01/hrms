using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.CreateDepartment;

public record CreateDepartmentCommand : IRequest<DepartmentDto>
{
    public Guid CompanyId { get; init; }
    public Guid? BranchId { get; init; }
    public Guid? ParentDepartmentId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid? ManagerId { get; init; }
    public Guid? HODId { get; init; }
    public string Type { get; init; } = "Functional";
    public Guid TenantId { get; init; }
}
