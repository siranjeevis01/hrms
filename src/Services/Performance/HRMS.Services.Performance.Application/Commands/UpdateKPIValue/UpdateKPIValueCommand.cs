using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateKPIValue;

public class UpdateKPIValueCommand : IRequest
{
    public Guid KPIId { get; set; }
    public decimal CurrentValue { get; set; }
}
