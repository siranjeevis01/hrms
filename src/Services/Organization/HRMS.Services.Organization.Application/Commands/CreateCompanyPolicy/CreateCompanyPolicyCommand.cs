using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.CreateCompanyPolicy;

public record CreateCompanyPolicyCommand : IRequest<CompanyPolicyDto>
{
    public Guid CompanyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Category { get; init; } = "Leave";
    public string Content { get; init; } = string.Empty;
    public DateTime EffectiveDate { get; init; }
    public DateTime? ExpiryDate { get; init; }
    public Guid TenantId { get; init; }
}
