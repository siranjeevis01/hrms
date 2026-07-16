using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.RejectRegularization;

public class RejectRegularizationCommand : IRequest<Unit>
{
    public Guid RegularizationId { get; set; }
    public Guid RejectedBy { get; set; }
    public string? RejectionReason { get; set; }
}
