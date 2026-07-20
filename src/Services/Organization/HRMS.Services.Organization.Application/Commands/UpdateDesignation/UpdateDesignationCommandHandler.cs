using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.UpdateDesignation;

public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand, DesignationDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDesignationCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DesignationDto> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
    {
        var designation = await _context.Designations
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Designation with ID {request.Id} not found.");

        designation.UpdateDetails(
            name: request.Name,
            code: request.Code,
            description: request.Description,
            level: request.Level,
            minSalary: request.MinSalary,
            maxSalary: request.MaxSalary);

        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == designation.CompanyId, cancellationToken);

        var dto = _mapper.Map<DesignationDto>(designation);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
