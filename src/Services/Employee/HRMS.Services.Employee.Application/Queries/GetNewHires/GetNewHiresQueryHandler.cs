using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetNewHires;

public class GetNewHiresQueryHandler : IRequestHandler<GetNewHiresQuery, List<EmployeeListDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetNewHiresQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeListDto>> Handle(GetNewHiresQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Employees
            .Where(e => e.JoiningDate >= request.FromDate && e.JoiningDate <= request.ToDate);

        if (request.DepartmentId.HasValue)
            query = query.Where(e => e.DepartmentId == request.DepartmentId.Value);

        var employees = await query
            .OrderBy(e => e.JoiningDate)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EmployeeListDto>>(employees);
    }
}
