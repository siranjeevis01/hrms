using HRMS.Services.Chat.Application.DTOs;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetConversationSummaries;

public class GetConversationSummariesQuery : IRequest<List<ConversationSummaryDto>>
{
    public Guid EmployeeId { get; set; }
}
