using HRMS.Services.Workflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Workflow.Application.Interfaces;

public interface IWorkflowDbContext
{
    DbSet<WorkflowDefinition> WorkflowDefinitions { get; }
    DbSet<WorkflowStep> WorkflowSteps { get; }
    DbSet<WorkflowInstance> WorkflowInstances { get; }
    DbSet<WorkflowAction> WorkflowActions { get; }
    DbSet<ApprovalMatrix> ApprovalMatrices { get; }
    DbSet<Delegation> Delegates { get; }
    DbSet<NotificationRule> NotificationRules { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
