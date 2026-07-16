using MediatR;

namespace HRMS.Services.Employee.Application.Commands.AddBankDetail;

public class AddBankDetailCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string BankName { get; set; } = string.Empty;
    public string BankCode { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string AccountHolderName { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public string? TaxJurisdiction { get; set; }
    public string? Currency { get; set; }
}
