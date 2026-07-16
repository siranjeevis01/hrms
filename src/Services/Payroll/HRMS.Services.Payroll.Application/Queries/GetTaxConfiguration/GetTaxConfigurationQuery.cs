using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetTaxConfiguration;

public class GetTaxConfigurationQuery : IRequest<TaxConfigurationDto?>
{
    public Guid CompanyId { get; set; }
    public string FinancialYear { get; set; } = string.Empty;
}
