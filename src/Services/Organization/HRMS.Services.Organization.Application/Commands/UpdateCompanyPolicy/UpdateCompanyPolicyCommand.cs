using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.UpdateCompanyPolicy;

public record UpdateCompanyPolicyCommand : IRequest<CompanyPolicyDto>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Category { get; init; }
    public string? Content { get; init; }
    public DateTime? EffectiveDate { get; init; }
    public DateTime? ExpiryDate { get; init; }
}
