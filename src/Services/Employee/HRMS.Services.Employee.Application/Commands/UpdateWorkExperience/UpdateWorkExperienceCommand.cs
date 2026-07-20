using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateWorkExperience;

public class UpdateWorkExperienceCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? CompanyName { get; set; }
    public string? Designation { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public bool? IsCurrent { get; set; }
    public string? ReasonForLeaving { get; set; }
    public string? ReferenceName { get; set; }
    public string? ReferencePhone { get; set; }
}
