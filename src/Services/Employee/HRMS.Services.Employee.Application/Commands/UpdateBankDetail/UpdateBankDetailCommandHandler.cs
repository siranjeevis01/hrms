using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateBankDetail;

public class UpdateBankDetailCommandHandler : IRequestHandler<UpdateBankDetailCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateBankDetailCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBankDetailCommand request, CancellationToken cancellationToken)
    {
        var bankDetail = await _context.BankDetails
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Bank detail with ID {request.Id} not found.");

        if (request.IsPrimary == true)
        {
            var existingPrimary = await _context.BankDetails
                .Where(b => b.EmployeeId == bankDetail.EmployeeId && b.IsPrimary && b.Id != request.Id)
                .ToListAsync(cancellationToken);

            foreach (var bd in existingPrimary)
            {
                bd.Update(null, null, null, null, false, null, null);
            }
        }

        bankDetail.Update(
            request.BankName, request.BankCode, request.AccountNumber,
            request.AccountHolderName, request.IsPrimary, request.TaxJurisdiction,
            request.Currency);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
