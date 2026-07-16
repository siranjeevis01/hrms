using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.ShortlistApplication;

public class ShortlistApplicationCommand : IRequest<Unit>
{
    public Guid JobApplicationId { get; set; }
    public Guid AssignedTo { get; set; }
}
