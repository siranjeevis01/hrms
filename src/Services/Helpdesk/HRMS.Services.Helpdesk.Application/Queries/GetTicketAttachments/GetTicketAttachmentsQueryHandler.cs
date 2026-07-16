using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTicketAttachments;

public class GetTicketAttachmentsQueryHandler : IRequestHandler<GetTicketAttachmentsQuery, List<TicketAttachmentDto>>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetTicketAttachmentsQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TicketAttachmentDto>> Handle(GetTicketAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var attachments = await _context.TicketAttachments
            .Where(a => a.TicketId == request.TicketId)
            .OrderBy(a => a.UploadedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TicketAttachmentDto>>(attachments);
    }
}
