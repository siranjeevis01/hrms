using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.ScreenApplication;

public class ScreenApplicationCommand : IRequest<Unit>
{
    public Guid JobApplicationId { get; set; }
    public decimal ScreeningScore { get; set; }
    public string? Notes { get; set; }
}
