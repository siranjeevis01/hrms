using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CreateOffer;

public class CreateOfferCommand : IRequest<Guid>
{
    public Guid JobApplicationId { get; set; }
    public Guid CandidateId { get; set; }
    public string Position { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }
    public decimal CTC { get; set; }
    public decimal BasicSalary { get; set; }
    public DateTime JoiningDate { get; set; }
    public DateTime OfferExpiryDate { get; set; }
    public Guid TenantId { get; set; }
}
