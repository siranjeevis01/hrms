using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Domain.Entities;

namespace HRMS.Services.ProjectTask.Application.Mappings;

public class ProjectTaskMappingProfile : Profile
{
    public ProjectTaskMappingProfile()
    {
        CreateMap<Domain.Entities.Project, ProjectDto>();
        CreateMap<Domain.Entities.Project, ProjectListDto>()
            .ForMember(dest => dest.MemberCount, opt => opt.MapFrom(src => src.Members.Count));
        CreateMap<ProjectMember, ProjectMemberDto>();
        CreateMap<Epic, EpicDto>();
        CreateMap<Story, StoryDto>();
        CreateMap<TaskItem, TaskItemDto>();
        CreateMap<Bug, BugDto>();
        CreateMap<Sprint, SprintDto>();
        CreateMap<Board, BoardDto>();
        CreateMap<Comment, CommentDto>();
        CreateMap<TimeLog, TimeLogDto>();
    }
}
