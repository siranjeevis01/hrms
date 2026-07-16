using HRMS.Services.Travel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Interfaces;

public interface ITravelDbContext
{
    DbSet<TravelRequest> TravelRequests { get; }
    DbSet<TravelItinerary> TravelItineraries { get; }
    DbSet<TravelExpense> TravelExpenses { get; }
    DbSet<TravelApproval> TravelApprovals { get; }
    DbSet<VisaRequest> VisaRequests { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
