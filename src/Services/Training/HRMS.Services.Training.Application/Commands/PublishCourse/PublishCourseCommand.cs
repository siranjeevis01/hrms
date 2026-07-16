using MediatR;

namespace HRMS.Services.Training.Application.Commands.PublishCourse;

public class PublishCourseCommand : IRequest
{
    public Guid Id { get; set; }
}
