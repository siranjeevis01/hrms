using HRMS.Services.Travel.Domain.Enums;
using MediatR;

namespace HRMS.Services.Travel.Application.Commands.UpdateVisaRequest;

public class UpdateVisaRequestCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Country { get; set; }
    public string? VisaType { get; set; }
    public string? Purpose { get; set; }
    public Guid? TravelRequestId { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? DocumentUrl { get; set; }
    public VisaStatus? Status { get; set; }
}
