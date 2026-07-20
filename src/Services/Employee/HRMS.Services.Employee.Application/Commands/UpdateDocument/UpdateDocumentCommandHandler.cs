using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UpdateDocument;

public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public UpdateDocumentCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Document with ID {request.Id} not found.");

        document.UpdateDetails(
            request.DocumentType, request.FileName, request.FileUrl,
            request.ExpiryDate);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
