using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.CreateCompany;

public record CreateCompanyCommand : IRequest<CompanyDto>
{
    public string Name { get; init; } = string.Empty;
    public string LegalName { get; init; } = string.Empty;
    public string RegistrationNumber { get; init; } = string.Empty;
    public string TaxId { get; init; } = string.Empty;
    public string? LogoUrl { get; init; }
    public string? Website { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public AddressDto? Address { get; init; }
    public DateTime? FoundedDate { get; init; }
    public string? Industry { get; init; }
    public string? EmployeeCountRange { get; init; }
    public Guid TenantId { get; init; }
}
