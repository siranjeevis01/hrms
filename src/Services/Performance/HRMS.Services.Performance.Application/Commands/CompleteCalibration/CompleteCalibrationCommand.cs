using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CompleteCalibration;

public class CompleteCalibrationCommand : IRequest
{
    public Guid SessionId { get; set; }
}
