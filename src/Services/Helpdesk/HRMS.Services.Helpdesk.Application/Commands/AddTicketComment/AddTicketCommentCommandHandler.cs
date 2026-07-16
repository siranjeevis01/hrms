using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Entities;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.AddTicketComment;

public class AddTicketCommentCommandHandler : IRequestHandler<AddTicketCommentCommand, Guid>
{
    private readonly IHelpdeskDbContext _context;

    public AddTicketCommentCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddTicketCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = TicketComment.Create(
            request.TicketId,
            request.EmployeeId,
            request.Content,
            request.IsInternal,
            request.TenantId);

        _context.TicketComments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
