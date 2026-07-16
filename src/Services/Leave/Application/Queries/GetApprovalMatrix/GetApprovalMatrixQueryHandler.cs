using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetApprovalMatrix;

public class GetApprovalMatrixQueryHandler : IRequestHandler<GetApprovalMatrixQuery, List<LeaveApprovalMatrixDto>>
{
    private readonly ILeaveDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetApprovalMatrixQueryHandler(ILeaveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<LeaveApprovalMatrixDto>> Handle(GetApprovalMatrixQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var query = _context.LeaveApprovalMatrices
            .Where(m => m.TenantId == tenantId);

        if (request.LeaveTypeId.HasValue)
            query = query.Where(m => m.LeaveTypeId == request.LeaveTypeId.Value);

        query = query.Where(m => m.CompanyId == request.CompanyId);

        var matrices = await query
            .OrderBy(m => m.LeaveTypeId)
            .ThenBy(m => m.Level)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<LeaveApprovalMatrixDto>>(matrices);

        var leaveTypes = await _context.LeaveTypes
            .Where(lt => lt.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        foreach (var dto in dtos)
        {
            var lt = leaveTypes.FirstOrDefault(x => x.Id == dto.LeaveTypeId);
            if (lt != null)
                dto.LeaveTypeName = lt.Name;
        }

        return dtos;
    }
}
