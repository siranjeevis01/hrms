using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.UpdateDepartment;

public record UpdateDepartmentCommand : IRequest<DepartmentDto>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Code { get; init; }
    public string? Description { get; init; }
    public Guid? ManagerId { get; init; }
    public Guid? HODId { get; init; }
    public Guid? BranchId { get; init; }
    public Guid? ParentDepartmentId { get; init; }
    public string? Type { get; init; }
}
