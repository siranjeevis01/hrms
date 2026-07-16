using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UploadDocument;

public class UploadDocumentCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public DocumentType DocumentType { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string MimeType { get; set; } = string.Empty;
    public DateTime? ExpiryDate { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
