using HRMS.Services.Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.DeleteDocument;

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, Unit>
{
    private readonly IEmployeeDbContext _context;

    public DeleteDocumentCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Document with ID {request.Id} not found.");

        _context.Documents.Remove(document);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
