using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateEducation;

public class UpdateEducationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? Institution { get; set; }
    public string? Degree { get; set; }
    public string? FieldOfStudy { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Grade { get; set; }
    public decimal? Percentage { get; set; }
    public bool? IsHighest { get; set; }
    public string? Country { get; set; }
    public string? University { get; set; }
}
