using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetMyLeaveBalance;

public class GetMyLeaveBalanceQueryHandler : IRequestHandler<GetMyLeaveBalanceQuery, List<LeaveBalanceDto>>
{
    private readonly ILeaveDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetMyLeaveBalanceQueryHandler(ILeaveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<LeaveBalanceDto>> Handle(GetMyLeaveBalanceQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;
        var year = request.Year ?? DateTime.UtcNow.Year;

        var balances = await _context.LeaveBalances
            .Where(lb => lb.EmployeeId == request.EmployeeId && lb.Year == year && lb.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<LeaveBalanceDto>>(balances);

        var leaveTypes = await _context.LeaveTypes
            .Where(lt => lt.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        foreach (var dto in dtos)
        {
            var leaveType = leaveTypes.FirstOrDefault(lt => lt.Id == dto.LeaveTypeId);
            if (leaveType != null)
            {
                dto.LeaveTypeName = leaveType.Name;
                dto.LeaveTypeCode = leaveType.Code;
            }
        }

        return dtos;
    }
}
