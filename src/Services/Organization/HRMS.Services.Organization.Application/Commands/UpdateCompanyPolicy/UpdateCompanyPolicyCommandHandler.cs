using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.UpdateCompanyPolicy;

public class UpdateCompanyPolicyCommandHandler : IRequestHandler<UpdateCompanyPolicyCommand, CompanyPolicyDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCompanyPolicyCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyPolicyDto> Handle(UpdateCompanyPolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = await _context.CompanyPolicies
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Company policy with ID {request.Id} not found.");

        PolicyCategory? category = null;
        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            category = Enum.TryParse<PolicyCategory>(request.Category, true, out var parsedCategory)
                ? parsedCategory
                : (PolicyCategory?)null;
        }

        policy.UpdateDetails(
            name: request.Name,
            description: request.Description,
            category: category,
            content: request.Content,
            effectiveDate: request.EffectiveDate,
            expiryDate: request.ExpiryDate);

        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == policy.CompanyId, cancellationToken);

        var dto = _mapper.Map<CompanyPolicyDto>(policy);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
