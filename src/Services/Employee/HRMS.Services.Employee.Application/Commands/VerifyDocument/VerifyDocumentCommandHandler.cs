using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.VerifyDocument;

public class VerifyDocumentCommandHandler : IRequestHandler<VerifyDocumentCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public VerifyDocumentCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(VerifyDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.DocumentId, cancellationToken)
            ?? throw new InvalidOperationException($"Document with ID {request.DocumentId} not found.");

        document.Verify(request.VerifiedBy);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
