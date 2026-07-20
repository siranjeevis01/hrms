using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Domain.Entities;

public class LeaveComment : BaseEntity
{
    private LeaveComment() { }

    public Guid LeaveApplicationId { get; private set; }
    public Guid UserId { get; private set; }
    public string Comment { get; private set; } = string.Empty;
    public new DateTime CreatedAt { get; private set; }

    public static LeaveComment Create(Guid id, Guid leaveApplicationId, Guid userId, string comment)
    {
        return new LeaveComment
        {
            Id = id,
            LeaveApplicationId = leaveApplicationId,
            UserId = userId,
            Comment = comment,
            CreatedAt = DateTime.UtcNow
        };
    }
}
