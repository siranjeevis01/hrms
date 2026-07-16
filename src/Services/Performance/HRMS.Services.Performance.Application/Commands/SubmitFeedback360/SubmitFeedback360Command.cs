using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitFeedback360;

public class SubmitFeedback360Command : IRequest
{
    public Guid FeedbackId { get; set; }
}
