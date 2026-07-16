using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Entities;
using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.CreateTicketCategory;

public class CreateTicketCategoryCommandHandler : IRequestHandler<CreateTicketCategoryCommand, Guid>
{
    private readonly IHelpdeskDbContext _context;

    public CreateTicketCategoryCommandHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTicketCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = TicketCategoryEntity.Create(
            request.Name,
            request.Code,
            request.Description,
            request.DefaultAssigneeId,
            request.SLAHours,
            request.TenantId);

        _context.TicketCategories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
