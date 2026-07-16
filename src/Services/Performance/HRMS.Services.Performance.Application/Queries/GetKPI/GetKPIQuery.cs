using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetKPI;

public class GetKPIQuery : IRequest<KPIDto?>
{
    public Guid Id { get; set; }
}
