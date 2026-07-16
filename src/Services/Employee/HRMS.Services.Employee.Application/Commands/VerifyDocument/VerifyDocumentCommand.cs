using MediatR;

namespace HRMS.Services.Employee.Application.Commands.VerifyDocument;

public class VerifyDocumentCommand : IRequest<Unit>
{
    public Guid DocumentId { get; set; }
    public Guid VerifiedBy { get; set; }
}
