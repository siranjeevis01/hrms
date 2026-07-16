using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetAppraisal;

public class GetAppraisalQuery : IRequest<AppraisalDto?>
{
    public Guid Id { get; set; }
}
