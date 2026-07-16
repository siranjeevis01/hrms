using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Queries.GetCompanyPolicies;

public record GetCompanyPoliciesQuery : IRequest<List<CompanyPolicyDto>>
{
    public Guid CompanyId { get; init; }
    public string? Category { get; init; }
    public bool? IsActive { get; init; }
}
