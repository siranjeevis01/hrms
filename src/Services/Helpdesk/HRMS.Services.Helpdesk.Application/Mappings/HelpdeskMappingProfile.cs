using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Domain.Entities;

namespace HRMS.Services.Helpdesk.Application.Mappings;

public class HelpdeskMappingProfile : Profile
{
    public HelpdeskMappingProfile()
    {
        CreateMap<HelpdeskTicket, HelpdeskTicketDto>();
        CreateMap<TicketComment, TicketCommentDto>();
        CreateMap<TicketAttachment, TicketAttachmentDto>();
        CreateMap<TicketCategoryEntity, TicketCategoryDto>();
        CreateMap<KnowledgeArticle, KnowledgeArticleDto>();
        CreateMap<Faq, FaqDto>();
    }
}
