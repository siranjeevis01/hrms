using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.DeleteBankDetail;

public class DeleteBankDetailCommandHandler : IRequestHandler<DeleteBankDetailCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public DeleteBankDetailCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteBankDetailCommand request, CancellationToken cancellationToken)
    {
        var bankDetail = await _context.BankDetails
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Bank detail with ID {request.Id} not found.");

        _context.BankDetails.Remove(bankDetail);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
