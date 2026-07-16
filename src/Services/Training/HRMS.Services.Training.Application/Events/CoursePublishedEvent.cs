namespace HRMS.Services.Training.Application.Events;

public class CoursePublishedEvent
{
    public Guid CourseId { get; }
    public string Title { get; }
    public DateTime PublishedAt { get; }

    public CoursePublishedEvent(Guid courseId, string title)
    {
        CourseId = courseId;
        Title = title;
        PublishedAt = DateTime.UtcNow;
    }
}
