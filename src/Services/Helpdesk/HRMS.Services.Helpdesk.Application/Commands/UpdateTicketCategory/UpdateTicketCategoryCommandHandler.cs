using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Commands.UpdateTicketCategory;

public class UpdateTicketCategoryCommandHandler : IRequestHandler<UpdateTicketCategoryCommand>
{
    private readonly IHelpdeskDbContext _context;

    public UpdateTicketCategoryCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTicketCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.TicketCategories
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category == null)
            throw new InvalidOperationException($"Ticket category with ID {request.Id} not found.");

        category.Update(request.Name, request.Code, request.Description, request.DefaultAssigneeId, request.SLAHours, request.IsActive);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
