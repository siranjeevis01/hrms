using HRMS.Services.Chat.Application.Interfaces;
using HRMS.Services.Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Infrastructure.Persistence;

public class ChatDbContext : DbContext, IChatDbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<ConversationParticipant> ConversationParticipants => Set<ConversationParticipant>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<MessageReaction> MessageReactions => Set<MessageReaction>();
    public DbSet<MessageRead> MessageReads => Set<MessageRead>();
    public DbSet<ChatChannel> ChatChannels => Set<ChatChannel>();
    public DbSet<UserPresence> UserPresences => Set<UserPresence>();
    public DbSet<ChatNotification> ChatNotifications => Set<ChatNotification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<HRMS.Shared.Kernel.Common.BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
