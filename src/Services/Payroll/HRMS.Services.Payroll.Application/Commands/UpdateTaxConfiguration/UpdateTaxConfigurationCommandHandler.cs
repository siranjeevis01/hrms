using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.UpdateTaxConfiguration;

public class UpdateTaxConfigurationCommandHandler : IRequestHandler<UpdateTaxConfigurationCommand, Guid>
{
    private readonly IPayrollDbContext _context;

    public UpdateTaxConfigurationCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateTaxConfigurationCommand request, CancellationToken cancellationToken)
    {
        var config = await _context.TaxConfigurations
            .FirstOrDefaultAsync(t => t.CompanyId == request.CompanyId
                && t.FinancialYear == request.FinancialYear
                && t.TenantId == request.TenantId, cancellationToken);

        if (config != null)
        {
            config.Update(request.TaxSlabConfig, request.PFContributionRate,
                request.ESIContributionRate, request.ProfessionalTaxConfig, request.StandardDeduction);
        }
        else
        {
            config = new TaxConfiguration(request.CompanyId, request.FinancialYear, request.Country,
                request.TaxSlabConfig, request.PFContributionRate, request.ESIContributionRate,
                request.ProfessionalTaxConfig, request.StandardDeduction, request.TenantId);
            _context.TaxConfigurations.Add(config);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return config.Id;
    }
}
