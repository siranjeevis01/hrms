using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Interfaces;

public interface IChatDbContext
{
    DbSet<Conversation> Conversations { get; }
    DbSet<ConversationParticipant> ConversationParticipants { get; }
    DbSet<Message> Messages { get; }
    DbSet<MessageReaction> MessageReactions { get; }
    DbSet<MessageRead> MessageReads { get; }
    DbSet<ChatChannel> ChatChannels { get; }
    DbSet<UserPresence> UserPresences { get; }
    DbSet<ChatNotification> ChatNotifications { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
