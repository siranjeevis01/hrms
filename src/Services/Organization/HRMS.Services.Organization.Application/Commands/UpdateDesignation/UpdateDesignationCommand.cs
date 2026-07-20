using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.UpdateDesignation;

public record UpdateDesignationCommand : IRequest<DesignationDto>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Code { get; init; }
    public string? Description { get; init; }
    public int? Level { get; init; }
    public decimal? MinSalary { get; init; }
    public decimal? MaxSalary { get; init; }
}
