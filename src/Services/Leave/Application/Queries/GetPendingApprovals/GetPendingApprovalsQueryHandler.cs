using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetPendingApprovals;

public class GetPendingApprovalsQueryHandler : IRequestHandler<GetPendingApprovalsQuery, List<LeaveApplicationListDto>>
{
    private readonly ILeaveDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetPendingApprovalsQueryHandler(ILeaveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<LeaveApplicationListDto>> Handle(GetPendingApprovalsQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var pendingLeaves = await _context.LeaveApplications
            .Where(la => la.TenantId == tenantId && la.Status == Domain.Enums.LeaveStatus.Pending)
            .OrderByDescending(la => la.AppliedAt)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<LeaveApplicationListDto>>(pendingLeaves);

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
