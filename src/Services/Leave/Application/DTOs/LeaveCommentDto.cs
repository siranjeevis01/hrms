using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Application.DTOs;

public class LeaveCommentDto : BaseDto
{
    public Guid LeaveApplicationId { get; set; }
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string Comment { get; set; } = string.Empty;
}
