using AutoMapper;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Domain.Entities;

namespace HRMS.Services.Recruitment.Application.Mappings;

public class RecruitmentMappingProfile : Profile
{
    public RecruitmentMappingProfile()
    {
        CreateMap<Candidate, CandidateDto>();
        CreateMap<JobPosting, JobPostingDto>();
        CreateMap<JobApplication, JobApplicationDto>()
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate != null ? src.Candidate.FirstName + " " + src.Candidate.LastName : null))
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPosting != null ? src.JobPosting.Title : null));
        CreateMap<Interview, InterviewDto>()
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate != null ? src.Candidate.FirstName + " " + src.Candidate.LastName : null));
        CreateMap<InterviewFeedback, InterviewFeedbackDto>();
        CreateMap<OfferLetter, OfferLetterDto>()
            .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.Candidate != null ? src.Candidate.FirstName + " " + src.Candidate.LastName : null));
        CreateMap<OnboardingChecklist, OnboardingChecklistDto>();
    }
}
