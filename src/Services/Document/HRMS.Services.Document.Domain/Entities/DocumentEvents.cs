using HRMS.Services.Document.Domain.Enums;
using MediatR;

namespace HRMS.Services.Document.Domain.Entities;

public record DocumentUploadedEvent(Guid DocumentId, string DocumentName, Guid UploadedBy, string TenantId) : INotification;

public record DocumentSharedEvent(Guid DocumentId, Guid SharedBy, Guid SharedWithUserId, DocumentPermission Permission, string TenantId) : INotification;

public record DocumentDeletedEvent(Guid DocumentId, Guid DeletedBy, string TenantId) : INotification;
