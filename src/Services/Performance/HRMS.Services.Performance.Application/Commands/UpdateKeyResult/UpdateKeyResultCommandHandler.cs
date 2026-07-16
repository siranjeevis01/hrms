using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateKeyResult;

public class UpdateKeyResultCommandHandler : IRequestHandler<UpdateKeyResultCommand>
{
    private readonly IPerformanceDbContext _context;

    public UpdateKeyResultCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateKeyResultCommand request, CancellationToken cancellationToken)
    {
        var keyResult = await _context.KeyResults.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new Exception($"Key Result with ID {request.Id} not found.");

        keyResult.Update(request.Title, request.Description, request.TargetValue, request.Unit, request.Weight);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
