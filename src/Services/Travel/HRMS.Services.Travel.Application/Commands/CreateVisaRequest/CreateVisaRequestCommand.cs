using MediatR;

namespace HRMS.Services.Travel.Application.Commands.CreateVisaRequest;

public class CreateVisaRequestCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Country { get; set; } = string.Empty;
    public string VisaType { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
    public Guid? TravelRequestId { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string? DocumentUrl { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
