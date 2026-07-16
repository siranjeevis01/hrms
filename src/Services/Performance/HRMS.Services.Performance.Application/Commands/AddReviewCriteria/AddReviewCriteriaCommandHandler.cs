using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.AddReviewCriteria;

public class AddReviewCriteriaCommandHandler : IRequestHandler<AddReviewCriteriaCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public AddReviewCriteriaCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddReviewCriteriaCommand request, CancellationToken cancellationToken)
    {
        var criteria = ReviewCriteria.Create(
            request.PerformanceReviewId,
            request.Category,
            request.CriteriaName,
            request.Weight,
            request.Comments,
            request.TenantId);

        _context.ReviewCriteria.Add(criteria);
        await _context.SaveChangesAsync(cancellationToken);

        return criteria.Id;
    }
}
