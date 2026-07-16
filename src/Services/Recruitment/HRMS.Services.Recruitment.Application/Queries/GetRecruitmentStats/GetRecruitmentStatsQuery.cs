using HRMS.Services.Recruitment.Application.DTOs;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Queries.GetRecruitmentStats;

public class GetRecruitmentStatsQuery : IRequest<RecruitmentStatsDto>
{
    public Guid? TenantId { get; set; }
}
