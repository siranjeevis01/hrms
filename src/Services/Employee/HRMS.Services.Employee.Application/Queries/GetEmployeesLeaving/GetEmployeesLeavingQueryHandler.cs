using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeesLeaving;

public class GetEmployeesLeavingQueryHandler : IRequestHandler<GetEmployeesLeavingQuery, List<EmployeeListDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeesLeavingQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeListDto>> Handle(GetEmployeesLeavingQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Employees
            .Where(e => e.EmploymentStatus == EmploymentStatus.OnNotice ||
                        e.EmploymentStatus == EmploymentStatus.Resigned ||
                        e.EmploymentStatus == EmploymentStatus.Terminated);

        if (request.DepartmentId.HasValue)
            query = query.Where(e => e.DepartmentId == request.DepartmentId.Value);

        var employees = await query
            .OrderBy(e => e.LastWorkingDate)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EmployeeListDto>>(employees);
    }
}
