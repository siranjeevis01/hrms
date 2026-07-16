using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetLeavePolicy;

public class GetLeavePolicyQueryHandler : IRequestHandler<GetLeavePolicyQuery, LeavePolicyDto?>
{
    private readonly ILeaveDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetLeavePolicyQueryHandler(ILeaveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<LeavePolicyDto?> Handle(GetLeavePolicyQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var policy = await _context.LeavePolicies
            .FirstOrDefaultAsync(p => p.CompanyId == request.CompanyId && p.TenantId == tenantId, cancellationToken);

        return policy != null ? _mapper.Map<LeavePolicyDto>(policy) : null;
    }
}
