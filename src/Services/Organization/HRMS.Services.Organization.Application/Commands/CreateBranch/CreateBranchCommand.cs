using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.CreateBranch;

public record CreateBranchCommand : IRequest<BranchDto>
{
    public Guid CompanyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public AddressDto? Address { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public Guid? ManagerId { get; init; }
    public bool IsHeadquarters { get; init; }
    public Guid TenantId { get; init; }
}
