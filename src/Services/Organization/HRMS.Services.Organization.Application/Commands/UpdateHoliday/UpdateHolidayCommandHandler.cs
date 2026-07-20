using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.UpdateHoliday;

public class UpdateHolidayCommandHandler : IRequestHandler<UpdateHolidayCommand, HolidayDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateHolidayCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HolidayDto> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
    {
        var holiday = await _context.Holidays
            .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Holiday with ID {request.Id} not found.");

        HolidayType? holidayType = null;
        if (!string.IsNullOrWhiteSpace(request.Type))
        {
            holidayType = Enum.TryParse<HolidayType>(request.Type, true, out var parsedType)
                ? parsedType
                : (HolidayType?)null;
        }

        holiday.UpdateDetails(
            name: request.Name,
            date: request.Date,
            type: holidayType,
            branchId: request.BranchId,
            isRecurring: request.IsRecurring,
            applicableDepartmentIdsJson: request.ApplicableDepartmentIdsJson);

        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == holiday.CompanyId, cancellationToken);

        var dto = _mapper.Map<HolidayDto>(holiday);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
