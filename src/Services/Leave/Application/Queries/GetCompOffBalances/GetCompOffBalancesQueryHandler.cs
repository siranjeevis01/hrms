using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetCompOffBalances;

public class GetCompOffBalancesQueryHandler : IRequestHandler<GetCompOffBalancesQuery, List<CompOffDto>>
{
    private readonly ILeaveDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetCompOffBalancesQueryHandler(ILeaveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<CompOffDto>> Handle(GetCompOffBalancesQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var query = _context.CompOffs
            .Where(co => co.EmployeeId == request.EmployeeId && co.TenantId == tenantId);

        if (!string.IsNullOrEmpty(request.Status) && Enum.TryParse<CompOffStatus>(request.Status, out var status))
            query = query.Where(co => co.Status == status);

        var compOffs = await query
            .OrderByDescending(co => co.EarnedDate)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CompOffDto>>(compOffs);
    }
}
