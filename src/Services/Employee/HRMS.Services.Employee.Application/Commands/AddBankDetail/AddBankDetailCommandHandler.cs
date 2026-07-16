using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.AddBankDetail;

public class AddBankDetailCommandHandler : IRequestHandler<AddBankDetailCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public AddBankDetailCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddBankDetailCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (!employeeExists)
            throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        if (request.IsPrimary)
        {
            var existingPrimary = await _context.BankDetails
                .Where(b => b.EmployeeId == request.EmployeeId && b.IsPrimary)
                .ToListAsync(cancellationToken);

            foreach (var bankDetail in existingPrimary)
            {
                bankDetail.Update(null, null, null, null, false, null, null);
            }
        }

        var bankDetail_ = BankDetail.Create(
            request.EmployeeId, request.BankName, request.BankCode,
            request.AccountNumber, request.AccountHolderName,
            request.IsPrimary, request.TaxJurisdiction, request.Currency);

        _context.BankDetails.Add(bankDetail_);
        await _context.SaveChangesAsync(cancellationToken);

        return bankDetail_.Id;
    }
}
