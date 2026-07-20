using MediatR;

namespace HRMS.Services.Employee.Application.Commands.UpdateCertification;

public class UpdateCertificationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? IssuingOrganization { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }
    public bool? DoesNotExpire { get; set; }
}
