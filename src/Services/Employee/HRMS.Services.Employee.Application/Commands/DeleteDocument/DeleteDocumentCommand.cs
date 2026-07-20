using MediatR;

namespace HRMS.Services.Employee.Application.Commands.DeleteDocument;

public class DeleteDocumentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
