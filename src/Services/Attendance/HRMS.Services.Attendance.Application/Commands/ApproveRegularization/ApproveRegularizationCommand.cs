using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.ApproveRegularization;

public class ApproveRegularizationCommand : IRequest<Unit>
{
    public Guid RegularizationId { get; set; }
    public Guid ApprovedBy { get; set; }
}
