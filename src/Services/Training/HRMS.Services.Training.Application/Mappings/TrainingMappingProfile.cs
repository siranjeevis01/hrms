using AutoMapper;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Domain.Entities;
using HRMS.Services.Training.Domain.Enums;

namespace HRMS.Services.Training.Application.Mappings;

public class TrainingMappingProfile : Profile
{
    public TrainingMappingProfile()
    {
        CreateMap<Course, CourseDto>()
            .ForMember(d => d.ModuleCount, o => o.Ignore())
            .ForMember(d => d.EnrollmentCount, o => o.Ignore())
            .ForMember(d => d.DifficultyLevel, o => o.MapFrom(s => s.DifficultyLevel.ToString()))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<CourseModule, CourseModuleDto>()
            .ForMember(d => d.LessonCount, o => o.Ignore());

        CreateMap<Lesson, LessonDto>()
            .ForMember(d => d.ContentType, o => o.MapFrom(s => s.ContentType.ToString()));

        CreateMap<Enrollment, EnrollmentDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<LessonProgress, LessonProgressDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<Assessment, AssessmentDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<AssessmentQuestion, AssessmentQuestionDto>()
            .ForMember(d => d.QuestionType, o => o.MapFrom(s => s.QuestionType.ToString()));

        CreateMap<AssessmentAttempt, AssessmentAttemptDto>();

        CreateMap<Certificate, CertificateDto>();

        CreateMap<LearningPath, LearningPathDto>()
            .ForMember(d => d.CourseCount, o => o.Ignore())
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<LearningPathCourse, LearningPathCourseDto>();

        CreateMap<TrainingSchedule, TrainingScheduleDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));
    }
}
