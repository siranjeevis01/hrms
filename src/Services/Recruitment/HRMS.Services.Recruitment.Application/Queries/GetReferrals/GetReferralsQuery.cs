using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Queries.GetReferrals;

public class GetReferralsQuery : IRequest<List<CandidateDto>>
{
    public Guid EmployeeId { get; set; }
}
