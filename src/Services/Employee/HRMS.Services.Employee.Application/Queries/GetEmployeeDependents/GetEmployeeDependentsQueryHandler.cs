using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeDependents;

public class GetEmployeeDependentsQueryHandler : IRequestHandler<GetEmployeeDependentsQuery, List<DependentDto>>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeDependentsQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DependentDto>> Handle(GetEmployeeDependentsQuery request, CancellationToken cancellationToken)
    {
        var dependents = await _context.Dependents
            .Where(d => d.EmployeeId == request.EmployeeId)
            .OrderBy(d => d.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<DependentDto>>(dependents);
    }
}
