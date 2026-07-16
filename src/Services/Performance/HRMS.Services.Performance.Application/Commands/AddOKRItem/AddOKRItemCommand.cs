using MediatR;

namespace HRMS.Services.Performance.Application.Commands.AddOKRItem;

public class AddOKRItemCommand : IRequest<Guid>
{
    public Guid OKRId { get; set; }
    public string ObjectiveTitle { get; set; } = string.Empty;
    public string? ObjectiveDescription { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
