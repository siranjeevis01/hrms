using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.UpdateCompany;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCompanyCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Company with ID {request.Id} not found.");

        company.UpdateDetails(
            name: request.Name,
            legalName: request.LegalName,
            website: request.Website,
            email: request.Email,
            phone: request.Phone,
            foundedDate: request.FoundedDate,
            industry: request.Industry,
            employeeCountRange: request.EmployeeCountRange,
            logoUrl: request.LogoUrl);

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

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }
}
