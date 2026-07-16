using MediatR;

namespace HRMS.Services.Performance.Application.Commands.AddFeedbackAnswer;

public class AddFeedbackAnswerCommand : IRequest<Guid>
{
    public Guid Feedback360Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public decimal? Rating { get; set; }
    public string? Comments { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
