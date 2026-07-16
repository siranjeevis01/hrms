using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.CreateGrade;

public record CreateGradeCommand : IRequest<GradeDto>
{
    public Guid CompanyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public decimal MinSalary { get; init; }
    public decimal MaxSalary { get; init; }
    public string? Benefits { get; init; }
    public Guid TenantId { get; init; }
}
