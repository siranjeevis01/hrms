using MediatR;
using HRMS.Services.Organization.Application.DTOs;

namespace HRMS.Services.Organization.Application.Commands.UpdateBranch;

public record UpdateBranchCommand : IRequest<BranchDto>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Code { get; init; }
    public AddressDto? Address { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public Guid? ManagerId { get; init; }
    public bool? IsHeadquarters { get; init; }
}
