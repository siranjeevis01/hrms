using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Queries.GetCompany;

public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyDto?>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetCompanyQueryHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyDto?> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        return company == null ? null : _mapper.Map<CompanyDto>(company);
    }
}
