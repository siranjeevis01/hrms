using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using HRMS.Services.Organization.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.CreateCompanyPolicy;

public class CreateCompanyPolicyCommandHandler : IRequestHandler<CreateCompanyPolicyCommand, CompanyPolicyDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public CreateCompanyPolicyCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyPolicyDto> Handle(CreateCompanyPolicyCommand request, CancellationToken cancellationToken)
    {
        var companyExists = await _context.Companies
            .AnyAsync(c => c.Id == request.CompanyId && c.IsActive, cancellationToken);

        if (!companyExists)
            throw new InvalidOperationException($"Active company with ID {request.CompanyId} not found.");

        var category = Enum.TryParse<PolicyCategory>(request.Category, true, out var parsedCategory)
            ? parsedCategory
            : PolicyCategory.Leave;

        var policy = CompanyPolicy.Create(
            request.CompanyId,
            request.Name,
            request.Content,
            category,
            request.EffectiveDate,
            request.TenantId,
            request.Description,
            request.ExpiryDate);

        _context.CompanyPolicies.Add(policy);
        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);

        var dto = _mapper.Map<CompanyPolicyDto>(policy);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
