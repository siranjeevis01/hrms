using AutoMapper;
using HRMS.Services.Workflow.Application.DTOs;
using HRMS.Services.Workflow.Domain.Entities;

namespace HRMS.Services.Workflow.Application.Mappings;

public class WorkflowMappingProfile : Profile
{
    public WorkflowMappingProfile()
    {
        CreateMap<WorkflowDefinition, WorkflowDefinitionDto>();
        CreateMap<WorkflowStep, WorkflowStepDto>();
        CreateMap<WorkflowInstance, WorkflowInstanceDto>();
        CreateMap<WorkflowAction, WorkflowActionDto>();
        CreateMap<ApprovalMatrix, ApprovalMatrixDto>();
        CreateMap<Delegation, DelegateDto>();
        CreateMap<NotificationRule, NotificationRuleDto>();
    }
}
