using MediatR;

namespace HRMS.Services.Employee.Application.Commands.DeleteBankDetail;

public class DeleteBankDetailCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
