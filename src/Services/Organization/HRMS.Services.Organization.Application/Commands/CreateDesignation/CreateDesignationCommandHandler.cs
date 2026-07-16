using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.CreateDesignation;

public class CreateDesignationCommandHandler : IRequestHandler<CreateDesignationCommand, DesignationDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public CreateDesignationCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DesignationDto> Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
    {
        var companyExists = await _context.Companies
            .AnyAsync(c => c.Id == request.CompanyId && c.IsActive, cancellationToken);

        if (!companyExists)
            throw new InvalidOperationException($"Active company with ID {request.CompanyId} not found.");

        var designation = Designation.Create(
            request.CompanyId,
            request.Name,
            request.Code,
            request.TenantId,
            request.Level,
            request.MinSalary,
            request.MaxSalary);

        designation.UpdateDetails(description: request.Description);

        _context.Designations.Add(designation);
        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);

        var dto = _mapper.Map<DesignationDto>(designation);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
