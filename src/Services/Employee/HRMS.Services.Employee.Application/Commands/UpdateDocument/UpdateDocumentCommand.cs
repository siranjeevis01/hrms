using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateDocument;

public class UpdateDocumentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public DocumentType? DocumentType { get; set; }
    public string? FileName { get; set; }
    public string? FileUrl { get; set; }
    public DateTime? ExpiryDate { get; set; }
}
