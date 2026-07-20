using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateBankDetail;

public class UpdateBankDetailCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? BankName { get; set; }
    public string? BankCode { get; set; }
    public string? AccountNumber { get; set; }
    public string? AccountHolderName { get; set; }
    public bool? IsPrimary { get; set; }
    public string? TaxJurisdiction { get; set; }
    public string? Currency { get; set; }
}
