using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetFeedback360;

public class GetFeedback360Query : IRequest<Feedback360Dto?>
{
    public Guid Id { get; set; }
}
