using HRMS.Services.Travel.Application.DTOs;
using MediatR;

namespace HRMS.Services.Travel.Application.Queries.GetTravelExpenses;

public class GetTravelExpensesQuery : IRequest<List<TravelExpenseDto>>
{
    public Guid TravelRequestId { get; set; }
}
