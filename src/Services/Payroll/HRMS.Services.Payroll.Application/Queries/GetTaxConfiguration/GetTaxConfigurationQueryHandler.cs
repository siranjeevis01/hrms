using AutoMapper;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetTaxConfiguration;

public class GetTaxConfigurationQueryHandler : IRequestHandler<GetTaxConfigurationQuery, TaxConfigurationDto?>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetTaxConfigurationQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaxConfigurationDto?> Handle(GetTaxConfigurationQuery request, CancellationToken cancellationToken)
    {
        var config = await _context.TaxConfigurations
            .FirstOrDefaultAsync(t => t.CompanyId == request.CompanyId
                && t.FinancialYear == request.FinancialYear, cancellationToken);

        return config == null ? null : _mapper.Map<TaxConfigurationDto>(config);
    }
}
