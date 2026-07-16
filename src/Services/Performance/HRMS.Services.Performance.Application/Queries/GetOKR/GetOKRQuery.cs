using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetOKR;

public class GetOKRQuery : IRequest<OKRDto?>
{
    public Guid Id { get; set; }
}
