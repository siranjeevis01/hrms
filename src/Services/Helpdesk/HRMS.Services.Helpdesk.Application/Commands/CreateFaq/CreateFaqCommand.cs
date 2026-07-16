using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateFaq;

public class CreateFaqCommand : IRequest<Guid>
{
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public int Order { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
