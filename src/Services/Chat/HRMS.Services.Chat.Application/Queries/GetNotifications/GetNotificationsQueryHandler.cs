using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetNotifications;

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, PagedResult<ChatNotificationDto>>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetNotificationsQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<ChatNotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ChatNotifications
            .Where(n => n.EmployeeId == request.EmployeeId);

        if (request.UnreadOnly)
            query = query.Where(n => !n.IsRead);

        query = query.OrderByDescending(n => n.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var notifications = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<ChatNotificationDto>>(notifications);

        return PagedResult<ChatNotificationDto>.Create(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}
