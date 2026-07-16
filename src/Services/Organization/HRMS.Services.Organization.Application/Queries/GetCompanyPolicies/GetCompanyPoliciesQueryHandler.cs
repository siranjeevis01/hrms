using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Queries.GetCompanyPolicies;

public class GetCompanyPoliciesQueryHandler : IRequestHandler<GetCompanyPoliciesQuery, List<CompanyPolicyDto>>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetCompanyPoliciesQueryHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CompanyPolicyDto>> Handle(GetCompanyPoliciesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.CompanyPolicies
            .AsNoTracking()
            .Where(p => p.CompanyId == request.CompanyId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Category) &&
            Enum.TryParse<PolicyCategory>(request.Category, true, out var category))
        {
            query = query.Where(p => p.Category == category);
        }

        if (request.IsActive.HasValue)
            query = query.Where(p => p.IsActive == request.IsActive.Value);

        var policies = await query
            .OrderByDescending(p => p.EffectiveDate)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<CompanyPolicyDto>>(policies);

        foreach (var dto in dtos)
        {
            dto.CompanyName = string.Empty;
        }

        return dtos;
    }
}
