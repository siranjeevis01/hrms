using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.AddOKRItem;

public class AddOKRItemCommandHandler : IRequestHandler<AddOKRItemCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public AddOKRItemCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddOKRItemCommand request, CancellationToken cancellationToken)
    {
        var item = OKRItem.Create(
            request.OKRId,
            request.ObjectiveTitle,
            request.ObjectiveDescription,
            request.TenantId);

        _context.OKRItems.Add(item);
        await _context.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}
