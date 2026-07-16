using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateKeyResult;

public class CreateKeyResultCommandHandler : IRequestHandler<CreateKeyResultCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public CreateKeyResultCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateKeyResultCommand request, CancellationToken cancellationToken)
    {
        var keyResult = KeyResult.Create(
            request.GoalId,
            request.Title,
            request.Description,
            request.TargetValue,
            request.Unit,
            request.Weight,
            request.TenantId);

        _context.KeyResults.Add(keyResult);
        await _context.SaveChangesAsync(cancellationToken);

        return keyResult.Id;
    }
}
