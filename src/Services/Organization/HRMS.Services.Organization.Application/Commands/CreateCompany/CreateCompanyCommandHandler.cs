using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Events;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using HRMS.Services.Organization.Domain.ValueObjects;

namespace HRMS.Services.Organization.Application.Commands.CreateCompany;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public CreateCompanyCommandHandler(
        IOrganizationDbContext context,
        IMapper mapper,
        IPublisher publisher)
    {
        _context = context;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = Company.Create(
            request.Name,
            request.LegalName,
            request.RegistrationNumber,
            request.TaxId,
            request.TenantId);

        if (request.Address != null)
        {
            var address = new Address(
                request.Address.Street,
                request.Address.City,
                request.Address.State,
                request.Address.Country,
                request.Address.PostalCode,
                request.Address.Latitude,
                request.Address.Longitude);
            company.UpdateAddress(address);
        }

        company.UpdateDetails(
            website: request.Website,
            email: request.Email,
            phone: request.Phone,
            foundedDate: request.FoundedDate,
            industry: request.Industry,
            employeeCountRange: request.EmployeeCountRange,
            logoUrl: request.LogoUrl);

        _context.Companies.Add(company);
        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new CompanyCreatedEvent
        {
            CompanyId = company.Id,
            Name = company.Name,
            LegalName = company.LegalName,
            TenantId = company.TenantId
        }, cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }
}
