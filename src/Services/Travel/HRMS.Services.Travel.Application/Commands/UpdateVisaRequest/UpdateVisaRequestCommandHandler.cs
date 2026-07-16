using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Commands.UpdateVisaRequest;

public class UpdateVisaRequestCommandHandler : IRequestHandler<UpdateVisaRequestCommand>
{
    private readonly ITravelDbContext _context;

    public UpdateVisaRequestCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateVisaRequestCommand request, CancellationToken cancellationToken)
    {
        var visaRequest = await _context.VisaRequests
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

        if (visaRequest == null)
            throw new InvalidOperationException($"Visa request with ID {request.Id} not found.");

        visaRequest.Update(request.Country, request.VisaType, request.Purpose, request.TravelRequestId, request.SubmissionDate, request.ExpiryDate, request.DocumentUrl, request.Status);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
