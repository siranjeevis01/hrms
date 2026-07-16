using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetLeaveReport;

public class GetLeaveReportQueryHandler : IRequestHandler<GetLeaveReportQuery, LeaveReportDto>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetLeaveReportQueryHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<LeaveReportDto> Handle(GetLeaveReportQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var applications = await _context.LeaveApplications
            .Where(la => la.TenantId == tenantId
                && la.StartDate <= request.ToDate && la.EndDate >= request.FromDate)
            .ToListAsync(cancellationToken);

        var leaveTypes = await _context.LeaveTypes
            .Where(lt => lt.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        var report = new LeaveReportDto
        {
            FromDate = request.FromDate,
            ToDate = request.ToDate
        };

        if (!string.IsNullOrEmpty(request.Department))
        {
            report.Department = request.Department;
        }

        var filtered = applications.AsEnumerable();

        if (!string.IsNullOrEmpty(request.Department))
        {
            report.DepartmentName = request.Department;
        }

        var leaveList = filtered.ToList();

        report.TotalLeavesTaken = leaveList
            .Where(la => la.Status == LeaveStatus.Approved)
            .Sum(la => la.TotalDays);

        report.EmployeeCount = leaveList.Select(la => la.EmployeeId).Distinct().Count();
        report.AverageLeavesPerEmployee = report.EmployeeCount > 0
            ? report.TotalLeavesTaken / report.EmployeeCount
            : 0;

        report.LeaveTypeBreakdown = leaveList
            .Where(la => la.Status == LeaveStatus.Approved)
            .GroupBy(la => la.LeaveTypeId)
            .ToDictionary(
                g => leaveTypes.FirstOrDefault(lt => lt.Id == g.Key)?.Name ?? "Unknown",
                g => g.Sum(la => la.TotalDays));

        report.PendingCount = leaveList.Count(la => la.Status == LeaveStatus.Pending);
        report.ApprovedCount = leaveList.Count(la => la.Status == LeaveStatus.Approved);
        report.RejectedCount = leaveList.Count(la => la.Status == LeaveStatus.Rejected);

        return report;
    }
}
