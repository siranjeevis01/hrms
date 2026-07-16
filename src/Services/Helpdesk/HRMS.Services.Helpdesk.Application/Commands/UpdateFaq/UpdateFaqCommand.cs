using MediatR;

namespace HRMS.Services.Helpdesk.Application.Commands.UpdateFaq;

public class UpdateFaqCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public Guid? CategoryId { get; set; }
    public int? Order { get; set; }
    public bool? IsActive { get; set; }
}
