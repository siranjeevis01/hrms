using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateFeedback360;

public class CreateFeedback360CommandHandler : IRequestHandler<CreateFeedback360Command, Guid>
{
    private readonly IPerformanceDbContext _context;

    public CreateFeedback360CommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateFeedback360Command request, CancellationToken cancellationToken)
    {
        var feedback = Feedback360.Create(
            request.EmployeeId,
            request.ReviewerId,
            request.ReviewPeriod,
            request.Relationship,
            request.TenantId);

        _context.Feedback360s.Add(feedback);
        await _context.SaveChangesAsync(cancellationToken);

        return feedback.Id;
    }
}
