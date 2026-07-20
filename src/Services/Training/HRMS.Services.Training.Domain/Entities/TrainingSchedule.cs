using HRMS.Services.Training.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Training.Domain.Entities;

public class TrainingSchedule : BaseEntity
{
    public Guid CourseId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string? Location { get; private set; }
    public int MaxParticipants { get; private set; }
    public string? InstructorName { get; private set; }
    public string? MeetingUrl { get; private set; }
    public new TrainingScheduleStatus Status { get; private set; }

    private TrainingSchedule() { }

    public static TrainingSchedule Create(
        Guid courseId,
        DateTime startDate,
        DateTime endDate,
        string? location,
        int maxParticipants,
        string? instructorName,
        string? meetingUrl,
        Guid tenantId)
    {
        return new TrainingSchedule
        {
            Id = Guid.NewGuid(),
            CourseId = courseId,
            StartDate = startDate,
            EndDate = endDate,
            Location = location,
            MaxParticipants = maxParticipants,
            InstructorName = instructorName,
            MeetingUrl = meetingUrl,
            Status = TrainingScheduleStatus.Scheduled,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        DateTime? startDate,
        DateTime? endDate,
        string? location,
        int? maxParticipants,
        string? instructorName,
        string? meetingUrl)
    {
        StartDate = startDate ?? StartDate;
        EndDate = endDate ?? EndDate;
        Location = location ?? Location;
        MaxParticipants = maxParticipants ?? MaxParticipants;
        InstructorName = instructorName ?? InstructorName;
        MeetingUrl = meetingUrl ?? MeetingUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Start()
    {
        Status = TrainingScheduleStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = TrainingScheduleStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = TrainingScheduleStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}
