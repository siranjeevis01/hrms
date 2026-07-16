using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.CreateShift;

public class CreateShiftCommandHandler : IRequestHandler<CreateShiftCommand, ShiftDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public CreateShiftCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShiftDto> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
    {
        var companyExists = await _context.Companies
            .AnyAsync(c => c.Id == request.CompanyId && c.IsActive, cancellationToken);

        if (!companyExists)
            throw new InvalidOperationException($"Active company with ID {request.CompanyId} not found.");

        var shift = Shift.Create(
            request.CompanyId,
            request.Name,
            request.Code,
            request.StartTime,
            request.EndTime,
            request.TenantId,
            request.BreakDuration,
            request.GraceMinutes,
            request.IsFlexible,
            request.MaxShifts);

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);

        var dto = _mapper.Map<ShiftDto>(shift);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
