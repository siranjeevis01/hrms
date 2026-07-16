using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Employee.Application.Commands.UploadDocument;

public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, Guid>
{
    private readonly IEmployeeDbContext _context;

    public UploadDocumentCommandHandler(IEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees
            .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);

        if (!employeeExists)
            throw new InvalidOperationException($"Employee with ID {request.EmployeeId} not found.");

        var document = EmployeeDocument.Create(
            request.EmployeeId, request.DocumentType, request.FileName,
            request.FileUrl, request.FileSize, request.MimeType,
            request.ExpiryDate, request.TenantId);

        _context.Documents.Add(document);
        await _context.SaveChangesAsync(cancellationToken);

        return document.Id;
    }
}
