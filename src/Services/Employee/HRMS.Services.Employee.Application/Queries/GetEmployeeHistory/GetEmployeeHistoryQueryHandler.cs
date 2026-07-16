using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeHistory;

public class GetEmployeeHistoryQueryHandler : IRequestHandler<GetEmployeeHistoryQuery, List<EmployeeHistoryDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeHistoryQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeHistoryDto>> Handle(GetEmployeeHistoryQuery request, CancellationToken cancellationToken)
    {
        var history = await _context.EmployeeHistories
            .Where(h => h.EmployeeId == request.EmployeeId)
            .OrderByDescending(h => h.ActionDate)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EmployeeHistoryDto>>(history);
    }
}
