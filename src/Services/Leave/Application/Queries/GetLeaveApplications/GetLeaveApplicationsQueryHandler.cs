using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetLeaveApplications;

public class GetLeaveApplicationsQueryHandler : IRequestHandler<GetLeaveApplicationsQuery, List<LeaveApplicationListDto>>
{
    private readonly ILeaveDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetLeaveApplicationsQueryHandler(ILeaveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<LeaveApplicationListDto>> Handle(GetLeaveApplicationsQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var query = _context.LeaveApplications
            .Where(la => la.TenantId == tenantId);

        if (request.EmployeeId.HasValue)
            query = query.Where(la => la.EmployeeId == request.EmployeeId.Value);

        if (request.LeaveTypeId.HasValue)
            query = query.Where(la => la.LeaveTypeId == request.LeaveTypeId.Value);

        if (!string.IsNullOrEmpty(request.Status) && Enum.TryParse<LeaveStatus>(request.Status, out var status))
            query = query.Where(la => la.Status == status);

        if (request.FromDate.HasValue)
            query = query.Where(la => la.StartDate >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(la => la.EndDate <= request.ToDate.Value);

        var applications = await query
            .OrderByDescending(la => la.AppliedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<LeaveApplicationListDto>>(applications);

        var leaveTypes = await _context.LeaveTypes
            .Where(lt => lt.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        foreach (var dto in dtos)
        {
            var lt = leaveTypes.FirstOrDefault(x => x.Id == dto.LeaveTypeId);
            if (lt != null)
            {
                dto.LeaveTypeName = lt.Name;
                dto.LeaveTypeColor = lt.Color;
            }
        }

        return dtos;
    }
}
