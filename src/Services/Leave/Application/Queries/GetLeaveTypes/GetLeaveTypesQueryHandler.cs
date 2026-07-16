using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly ILeaveDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetLeaveTypesQueryHandler(ILeaveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var query = _context.LeaveTypes
            .Where(lt => lt.TenantId == tenantId);

        if (!request.IncludeInactive.HasValue || !request.IncludeInactive.Value)
            query = query.Where(lt => lt.IsActive);

        var leaveTypes = await query
            .OrderBy(lt => lt.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
    }
}
