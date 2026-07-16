using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.ApproveWorkFromHome;

public class ApproveWorkFromHomeCommand : IRequest<Unit>
{
    public Guid WorkFromHomeId { get; set; }
    public Guid ApprovedBy { get; set; }
    public bool IsApproved { get; set; } = true;
}
