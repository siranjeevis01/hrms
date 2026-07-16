using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Domain.Entities;

namespace HRMS.Services.Performance.Application.Mappings;

public class PerformanceMappingProfile : Profile
{
    public PerformanceMappingProfile()
    {
        CreateMap<Goal, GoalDto>();
        CreateMap<KeyResult, KeyResultDto>();
        CreateMap<OKR, OKRDto>();
        CreateMap<OKRItem, OKRItemDto>();
        CreateMap<KPI, KPIDto>();
        CreateMap<PerformanceReview, PerformanceReviewDto>();
        CreateMap<ReviewCriteria, ReviewCriteriaDto>();
        CreateMap<Feedback360, Feedback360Dto>();
        CreateMap<FeedbackAnswer, FeedbackAnswerDto>();
        CreateMap<CalibrationSession, CalibrationSessionDto>();
        CreateMap<CalibrationEntry, CalibrationEntryDto>();
        CreateMap<Appraisal, AppraisalDto>();
    }
}
