using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeSalary;

public class GetEmployeeSalaryQueryHandler : IRequestHandler<GetEmployeeSalaryQuery, SalaryStructureDto?>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeSalaryQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SalaryStructureDto?> Handle(GetEmployeeSalaryQuery request, CancellationToken cancellationToken)
    {
        var salary = await _context.SalaryStructures
            .Where(s => s.EmployeeId == request.EmployeeId && s.IsCurrent)
            .FirstOrDefaultAsync(cancellationToken);

        if (salary == null)
            return null;

        return _mapper.Map<SalaryStructureDto>(salary);
    }
}
