using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetDepartmentHeadCount;

public class GetDepartmentHeadCountQueryHandler : IRequestHandler<GetDepartmentHeadCountQuery, List<DepartmentHeadCountDto>>
{
    private readonly IEmployeeDbContext _context;

    public GetDepartmentHeadCountQueryHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentHeadCountDto>> Handle(GetDepartmentHeadCountQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Employees.AsQueryable();

        if (request.CompanyId.HasValue)
            query = query.Where(e => e.CompanyId == request.CompanyId.Value);

        var headCounts = await query
            .GroupBy(e => e.DepartmentId)
            .Select(g => new DepartmentHeadCountDto
            {
                DepartmentId = g.Key,
                HeadCount = g.Count(),
                ActiveCount = g.Count(e => e.EmploymentStatus == EmploymentStatus.Active),
                OnNoticeCount = g.Count(e => e.EmploymentStatus == EmploymentStatus.OnNotice)
            })
            .OrderByDescending(d => d.HeadCount)
            .ToListAsync(cancellationToken);

        return headCounts;
    }
}
