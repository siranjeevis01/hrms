using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Domain.Entities;

namespace HRMS.Services.Chat.Application.Mappings;

public class ChatMappingProfile : Profile
{
    public ChatMappingProfile()
    {
        CreateMap<Conversation, ConversationDto>();
        CreateMap<ConversationParticipant, ConversationParticipantDto>();
        CreateMap<Message, MessageDto>();
        CreateMap<MessageReaction, MessageReactionDto>();
        CreateMap<ChatChannel, ChatChannelDto>();
        CreateMap<UserPresence, UserPresenceDto>();
        CreateMap<ChatNotification, ChatNotificationDto>();
    }
}
