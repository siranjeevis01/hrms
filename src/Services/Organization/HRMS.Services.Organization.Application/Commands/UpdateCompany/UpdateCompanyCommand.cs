using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.UpdateCompany;

public record UpdateCompanyCommand : IRequest<CompanyDto>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? LegalName { get; init; }
    public string? RegistrationNumber { get; init; }
    public string? TaxId { get; init; }
    public string? LogoUrl { get; init; }
    public string? Website { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public AddressDto? Address { get; init; }
    public DateTime? FoundedDate { get; init; }
    public string? Industry { get; init; }
    public string? EmployeeCountRange { get; init; }
}
