using AutoMapper;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeeTaxDeclaration;

public class GetEmployeeTaxDeclarationQueryHandler : IRequestHandler<GetEmployeeTaxDeclarationQuery, EmployeeTaxDeclarationDto?>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeTaxDeclarationQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeTaxDeclarationDto?> Handle(GetEmployeeTaxDeclarationQuery request, CancellationToken cancellationToken)
    {
        var declaration = await _context.EmployeeTaxDeclarations
            .FirstOrDefaultAsync(d => d.EmployeeId == request.EmployeeId
                && d.FinancialYear == request.FinancialYear, cancellationToken);

        return declaration == null ? null : _mapper.Map<EmployeeTaxDeclarationDto>(declaration);
    }
}
