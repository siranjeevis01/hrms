using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Domain.Entities;
using MediatR;

namespace HRMS.Services.Travel.Application.Commands.CreateVisaRequest;

public class CreateVisaRequestCommandHandler : IRequestHandler<CreateVisaRequestCommand, Guid>
{
    private readonly ITravelDbContext _context;

    public CreateVisaRequestCommandHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateVisaRequestCommand request, CancellationToken cancellationToken)
    {
        var visaRequest = VisaRequest.Create(
            request.EmployeeId,
            request.Country,
            request.VisaType,
            request.Purpose,
            request.TravelRequestId,
            request.SubmissionDate,
            request.DocumentUrl,
            request.TenantId);

        _context.VisaRequests.Add(visaRequest);
        await _context.SaveChangesAsync(cancellationToken);

        return visaRequest.Id;
    }
}
