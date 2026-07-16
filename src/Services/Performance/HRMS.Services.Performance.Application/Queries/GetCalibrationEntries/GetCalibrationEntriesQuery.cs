using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetCalibrationEntries;

public class GetCalibrationEntriesQuery : IRequest<List<CalibrationEntryDto>>
{
    public Guid SessionId { get; set; }
}
