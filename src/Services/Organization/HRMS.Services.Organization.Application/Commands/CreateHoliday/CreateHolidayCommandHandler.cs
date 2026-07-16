using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Domain.Entities;
using HRMS.Services.Organization.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Commands.CreateHoliday;

public class CreateHolidayCommandHandler : IRequestHandler<CreateHolidayCommand, HolidayDto>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public CreateHolidayCommandHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HolidayDto> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
    {
        var companyExists = await _context.Companies
            .AnyAsync(c => c.Id == request.CompanyId && c.IsActive, cancellationToken);

        if (!companyExists)
            throw new InvalidOperationException($"Active company with ID {request.CompanyId} not found.");

        if (request.BranchId.HasValue)
        {
            var branchExists = await _context.Branches
                .AnyAsync(b => b.Id == request.BranchId.Value && b.IsActive, cancellationToken);

            if (!branchExists)
                throw new InvalidOperationException($"Branch with ID {request.BranchId} not found.");
        }

        var holidayType = Enum.TryParse<HolidayType>(request.Type, true, out var parsedType)
            ? parsedType
            : HolidayType.Public;

        var holiday = Holiday.Create(
            request.CompanyId,
            request.Name,
            request.Date,
            holidayType,
            request.TenantId,
            request.BranchId,
            request.IsRecurring,
            request.ApplicableDepartmentIdsJson);

        _context.Holidays.Add(holiday);
        await _context.SaveChangesAsync(cancellationToken);

        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);

        var dto = _mapper.Map<HolidayDto>(holiday);
        dto.CompanyName = company?.Name ?? string.Empty;
        return dto;
    }
}
