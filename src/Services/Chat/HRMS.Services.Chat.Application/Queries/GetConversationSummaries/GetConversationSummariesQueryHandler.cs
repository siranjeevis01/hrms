using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetConversationSummaries;

public class GetConversationSummariesQueryHandler : IRequestHandler<GetConversationSummariesQuery, List<ConversationSummaryDto>>
{
    private readonly IChatDbContext _context;

    public GetConversationSummariesQueryHandler(IChatDbContext context)
    {
        _context = context;
    }

    public async Task<List<ConversationSummaryDto>> Handle(GetConversationSummariesQuery request, CancellationToken cancellationToken)
    {
        var participations = await _context.ConversationParticipants
            .Where(p => p.EmployeeId == request.EmployeeId && p.LeftAt == null)
            .ToListAsync(cancellationToken);

        var conversationIds = participations.Select(p => p.ConversationId).ToList();

        var summaries = new List<ConversationSummaryDto>();

        foreach (var conversationId in conversationIds)
        {
            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(c => c.Id == conversationId, cancellationToken);

            if (conversation == null) continue;

            var participant = participations.First(p => p.ConversationId == conversationId);

            var lastMessage = await _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            var lastReadMessage = participant.LastReadMessageId.HasValue
                ? await _context.Messages
                    .Where(m => m.Id == participant.LastReadMessageId.Value)
                    .FirstOrDefaultAsync(cancellationToken)
                : null;

            var unreadCount = 0;
            if (lastReadMessage != null)
            {
                unreadCount = await _context.Messages
                    .CountAsync(m => m.ConversationId == conversationId
                        && m.CreatedAt > lastReadMessage.CreatedAt
                        && m.SenderId != request.EmployeeId
                        && !m.IsDeleted, cancellationToken);
            }

            summaries.Add(new ConversationSummaryDto
            {
                Id = conversation.Id,
                Name = conversation.Name,
                Type = conversation.Type,
                LastMessageAt = conversation.LastMessageAt,
                UnreadCount = unreadCount,
                LastMessageContent = lastMessage?.Content,
                LastMessageSenderId = lastMessage?.SenderId
            });
        }

        return summaries.OrderByDescending(s => s.LastMessageAt).ToList();
    }
}
