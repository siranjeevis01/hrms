using MediatR;

namespace HRMS.Services.Organization.Application.Commands.Delete;

public record DeleteCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string EntityType { get; init; } = string.Empty;
}

public enum EntityType
{
    Company,
    Branch,
    Department,
    Designation,
    Grade,
    Shift,
    Holiday,
    CompanyPolicy
}
