using AutoMapper;
using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Domain.Entities;
using NotificationEntity = HRMS.Services.Notification.Domain.Entities.Notification;

namespace HRMS.Services.Notification.Application.Mappings;

public class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<NotificationEntity, NotificationDto>();
        CreateMap<NotificationEntity, NotificationListDto>();
        CreateMap<NotificationTemplate, NotificationTemplateDto>();
        CreateMap<NotificationPreference, NotificationPreferenceDto>();
        CreateMap<NotificationDeliveryLog, DeliveryLogDto>();
    }
}
