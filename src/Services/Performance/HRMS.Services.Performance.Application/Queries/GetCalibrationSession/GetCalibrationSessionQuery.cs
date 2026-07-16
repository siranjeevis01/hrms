using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetCalibrationSession;

public class GetCalibrationSessionQuery : IRequest<CalibrationSessionDto?>
{
    public Guid Id { get; set; }
}
