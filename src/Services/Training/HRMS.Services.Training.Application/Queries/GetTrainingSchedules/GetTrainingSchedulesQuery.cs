using HRMS.Services.Training.Application.DTOs;
using MediatR;

namespace HRMS.Services.Training.Application.Queries.GetTrainingSchedules;

public class GetTrainingSchedulesQuery : IRequest<List<TrainingScheduleDto>>
{
    public Guid? CourseId { get; set; }
    public string? Status { get; set; }
}
