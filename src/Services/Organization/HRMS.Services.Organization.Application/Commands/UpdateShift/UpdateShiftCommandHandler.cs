using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.UpdateShift;

public class UpdateShiftCommandHandler : IRequestHandler<UpdateShiftCommand, ShiftDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateShiftCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShiftDto> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
    {
        var shift = await _context.Shifts
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Shift with ID {request.Id} not found.");

        shift.UpdateDetails(
            name: request.Name,
            code: request.Code,
            startTime: request.StartTime,
            endTime: request.EndTime,
            breakDuration: request.BreakDuration,
            graceMinutes: request.GraceMinutes,
            isFlexible: request.IsFlexible,
            maxShifts: request.MaxShifts);

        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == shift.CompanyId, cancellationToken);

        var dto = _mapper.Map<ShiftDto>(shift);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
