using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Queries.GetCompany;

public record GetCompanyQuery : IRequest<CompanyDto?>
{
    public Guid Id { get; init; }
}
