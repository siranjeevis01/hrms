using AutoMapper;
using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HRMS.Services.Employee.Application.Queries.GetEmployee;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto?>
{
    private readonly IEmployeeDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeQueryHandler(IEmployeeDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeDto?> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .Include(e => e.EmergencyContacts)
            .Include(e => e.Documents)
            .Include(e => e.BankDetails)
            .Include(e => e.Educations)
            .Include(e => e.WorkExperiences)
            .Include(e => e.Certifications)
            .Include(e => e.Skills)
            .Include(e => e.SalaryStructures)
            .Include(e => e.Dependents)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee == null)
            return null;

        return _mapper.Map<EmployeeDto>(employee);
    }
}
