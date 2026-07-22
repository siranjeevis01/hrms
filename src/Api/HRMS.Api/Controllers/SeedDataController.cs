using HRMS.Api;
using HRMS.Services.Attendance.Domain.Entities;
using HRMS.Services.Employee.Domain.Entities;
using HRMS.Services.Helpdesk.Domain.Entities;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Services.Organization.Domain.Entities;
using HRMS.Services.Organization.Domain.Enums;
using HRMS.Services.ProjectTask.Domain.Entities;
using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Services.Document.Domain.Entities;
using HRMS.Services.Document.Domain.Enums;
using HRMS.Services.Expense.Domain.Entities;
using HRMS.Services.Chat.Domain.Entities;
using HRMS.Services.Chat.Domain.Enums;
using HRMS.Services.Dashboard.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedDataController : ControllerBase
{
    private readonly HrmsDbContext _db;
    private readonly ILogger<SeedDataController> _logger;

    public SeedDataController(HrmsDbContext db, ILogger<SeedDataController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Seed([FromQuery] bool force = false, CancellationToken ct = default)
    {
        if (!force && await _db.Companies.AnyAsync(ct))
            return Ok(new { message = "Database already seeded. Use ?force=true to re-seed." });

        if (force)
        {
            _logger.LogInformation("Force re-seeding: clearing existing data...");
            _db.ChatChannels.RemoveRange(_db.ChatChannels);
            _db.Dashboards.RemoveRange(_db.Dashboards);
            _db.Documents.RemoveRange(_db.Documents);
            _db.ExpenseCategories.RemoveRange(_db.ExpenseCategories);
            _db.Projects.RemoveRange(_db.Projects);
            _db.TicketCategories.RemoveRange(_db.TicketCategories);
            _db.ShiftAssignments.RemoveRange(_db.ShiftAssignments);
            _db.AttendancePolicies.RemoveRange(_db.AttendancePolicies);
            _db.LeaveBalances.RemoveRange(_db.LeaveBalances);
            _db.LeaveTypes.RemoveRange(_db.LeaveTypes);
            _db.Employees.RemoveRange(_db.Employees);
            _db.Shifts.RemoveRange(_db.Shifts);
            _db.Designations.RemoveRange(_db.Designations);
            _db.Grades.RemoveRange(_db.Grades);
            _db.Departments.RemoveRange(_db.Departments);
            _db.Branches.RemoveRange(_db.Branches);
            _db.Companies.RemoveRange(_db.Companies);
            await _db.SaveChangesAsync(ct);
        }

        var tenantId = Guid.NewGuid();
        var tenantIdStr = tenantId.ToString();
        var now = DateTime.UtcNow;

        var company = Company.Create("Acme Corporation", "Acme Corporation Pvt Ltd", "ACM-2024-001", "TAX-123456789", tenantId);
        company.UpdateDetails(website: "https://acme.com", email: "hr@acme.com", phone: "+1-555-0100", industry: "Technology", employeeCountRange: "100-500");
        _db.Companies.Add(company);
        await _db.SaveChangesAsync(ct);

        var branches = new[]
        {
            Branch.Create(company.Id, "Headquarters", "HQ", tenantId, true),
            Branch.Create(company.Id, "Branch East", "BE", tenantId),
            Branch.Create(company.Id, "Branch West", "BW", tenantId)
        };
        _db.Branches.AddRange(branches);
        await _db.SaveChangesAsync(ct);

        var departments = new[]
        {
            Department.Create(company.Id, "Engineering", "ENG", tenantId, type: DepartmentType.Functional),
            Department.Create(company.Id, "Human Resources", "HR", tenantId, type: DepartmentType.Functional),
            Department.Create(company.Id, "Marketing", "MKT", tenantId, type: DepartmentType.Functional),
            Department.Create(company.Id, "Finance", "FIN", tenantId, type: DepartmentType.Functional),
            Department.Create(company.Id, "Operations", "OPS", tenantId, type: DepartmentType.Functional),
            Department.Create(company.Id, "Sales", "SAL", tenantId, type: DepartmentType.Functional),
            Department.Create(company.Id, "Design", "DSN", tenantId, type: DepartmentType.Functional),
            Department.Create(company.Id, "Quality Assurance", "QA", tenantId, type: DepartmentType.Functional)
        };
        _db.Departments.AddRange(departments);
        await _db.SaveChangesAsync(ct);

        var grades = new[]
        {
            Grade.Create(company.Id, "Junior", "G1", 25000, 45000, tenantId),
            Grade.Create(company.Id, "Mid-Level", "G2", 45000, 75000, tenantId),
            Grade.Create(company.Id, "Senior", "G3", 75000, 120000, tenantId),
            Grade.Create(company.Id, "Lead", "G4", 120000, 160000, tenantId),
            Grade.Create(company.Id, "Manager", "G5", 160000, 220000, tenantId),
            Grade.Create(company.Id, "Director", "G6", 220000, 350000, tenantId)
        };
        _db.Grades.AddRange(grades);
        await _db.SaveChangesAsync(ct);

        var designations = new[]
        {
            Designation.Create(company.Id, "Software Engineer", "SE", tenantId, level: 1),
            Designation.Create(company.Id, "Senior Software Engineer", "SSE", tenantId, level: 2),
            Designation.Create(company.Id, "Tech Lead", "TL", tenantId, level: 3),
            Designation.Create(company.Id, "Engineering Manager", "EM", tenantId, level: 4),
            Designation.Create(company.Id, "HR Manager", "HRM", tenantId, level: 4),
            Designation.Create(company.Id, "Marketing Specialist", "MKT-S", tenantId, level: 2),
            Designation.Create(company.Id, "Financial Analyst", "FA", tenantId, level: 2),
            Designation.Create(company.Id, "Product Manager", "PM", tenantId, level: 3),
            Designation.Create(company.Id, "UX Designer", "UXD", tenantId, level: 2),
            Designation.Create(company.Id, "QA Engineer", "QAE", tenantId, level: 1)
        };
        _db.Designations.AddRange(designations);
        await _db.SaveChangesAsync(ct);

        var shifts = new[]
        {
            Shift.Create(company.Id, "General Shift", "GS", new TimeOnly(9, 0), new TimeOnly(18, 0), tenantId),
            Shift.Create(company.Id, "Morning Shift", "MS", new TimeOnly(6, 0), new TimeOnly(15, 0), tenantId),
            Shift.Create(company.Id, "Evening Shift", "ES", new TimeOnly(14, 0), new TimeOnly(23, 0), tenantId)
        };
        _db.Shifts.AddRange(shifts);
        await _db.SaveChangesAsync(ct);

        var employeeNames = new[] {
            ("James", "Anderson", "james.anderson@acme.com"),
            ("Sarah", "Johnson", "sarah.johnson@acme.com"),
            ("Michael", "Williams", "michael.williams@acme.com"),
            ("Emily", "Brown", "emily.brown@acme.com"),
            ("David", "Davis", "david.davis@acme.com"),
            ("Lisa", "Miller", "lisa.miller@acme.com"),
            ("Robert", "Wilson", "robert.wilson@acme.com"),
            ("Jennifer", "Taylor", "jennifer.taylor@acme.com")
        };

        var employeeIds = new Guid[employeeNames.Length];
        for (int i = 0; i < employeeNames.Length; i++)
        {
            var (first, last, email) = employeeNames[i];
            employeeIds[i] = Guid.NewGuid();
            var emp = Employee.Create(
                employeeCode: $"EMP-{(i + 1):D4}",
                userId: employeeIds[i],
                companyId: company.Id,
                branchId: branches[0].Id,
                departmentId: departments[i % departments.Length].Id,
                designationId: designations[Math.Min(i, designations.Length - 1)].Id,
                gradeId: grades[Math.Min(i, grades.Length - 1)].Id,
                reportsToId: i > 0 ? employeeIds[0] : null,
                firstName: first,
                lastName: last,
                middleName: null,
                preferredName: null,
                email: email,
                personalEmail: null,
                phoneNumber: null,
                dateOfBirth: null,
                gender: null,
                maritalStatus: null,
                nationality: null,
                bloodGroup: null,
                profilePictureUrl: null,
                joiningDate: now.AddDays(-180 + (i * 20)),
                employmentType: HRMS.Services.Employee.Domain.Enums.EmploymentType.FullTime,
                tenantId: tenantIdStr);
            _db.Employees.Add(emp);
        }
        await _db.SaveChangesAsync(ct);

        var leaveTypes = new[]
        {
            LeaveType.Create(Guid.NewGuid(), "Annual Leave", "AL", "Paid annual leave", null, null, true, false, 20, 30, 5, 10, 12, true, true, true, 1, 30, null, false, null, GenderRestriction.All, null, AccrualType.Annual, 0, tenantId),
            LeaveType.Create(Guid.NewGuid(), "Sick Leave", "SL", "Medical leave", null, null, true, false, 12, 15, 0, 0, null, false, false, true, 1, 15, null, true, null, GenderRestriction.All, null, AccrualType.Monthly, 0, tenantId),
            LeaveType.Create(Guid.NewGuid(), "Personal Leave", "PL", "Personal time off", null, null, false, false, 5, 5, 0, 0, null, false, false, true, 1, 5, null, false, null, GenderRestriction.All, null, AccrualType.Monthly, 0, tenantId),
            LeaveType.Create(Guid.NewGuid(), "Maternity Leave", "ML", "Maternity leave", null, null, true, false, 90, 90, 0, 0, null, false, false, false, 1, 90, null, true, null, GenderRestriction.Female, null, AccrualType.Monthly, 0, tenantId),
            LeaveType.Create(Guid.NewGuid(), "Paternity Leave", "PAT", "Paternity leave", null, null, true, false, 15, 15, 0, 0, null, false, false, false, 1, 15, null, false, null, GenderRestriction.Male, null, AccrualType.Monthly, 0, tenantId),
            LeaveType.Create(Guid.NewGuid(), "Bereavement Leave", "BL", "Bereavement leave", null, null, true, false, 5, 5, 0, 0, null, false, false, false, 1, 5, null, false, null, GenderRestriction.All, null, AccrualType.Monthly, 0, tenantId)
        };
        _db.LeaveTypes.AddRange(leaveTypes);
        await _db.SaveChangesAsync(ct);

        foreach (var empId in employeeIds)
        {
            for (int i = 0; i < 3; i++)
            {
                _db.LeaveBalances.Add(LeaveBalance.Create(
                    Guid.NewGuid(), empId, leaveTypes[i].Id, now.Year, leaveTypes[i].DefaultBalanceDays, tenantId));
            }
        }
        await _db.SaveChangesAsync(ct);

        _db.AttendancePolicies.Add(AttendancePolicy.Create(
            company.Id, gracePeriodMinutes: 15, maxLateAllowed: 3, tenantId: tenantId));
        await _db.SaveChangesAsync(ct);

        foreach (var empId in employeeIds)
        {
            _db.ShiftAssignments.Add(ShiftAssignment.Create(
                empId, shifts[0].Id, now.AddDays(-180), tenantId: tenantId));
        }
        await _db.SaveChangesAsync(ct);

        _db.TicketCategories.AddRange(new[]
        {
            TicketCategoryEntity.Create("IT Support", "IT", "IT related issues", null, 24, tenantIdStr),
            TicketCategoryEntity.Create("Facilities", "FAC", "Facilities and infrastructure", null, 48, tenantIdStr),
            TicketCategoryEntity.Create("HR Inquiry", "HRI", "HR related queries", null, 24, tenantIdStr),
            TicketCategoryEntity.Create("Access Request", "ACR", "System access requests", null, 12, tenantIdStr)
        });

        var adminUserId = employeeIds[0];
        _db.Documents.AddRange(new[]
        {
            Document.Create("Employee Handbook", "employee-handbook.pdf", "application/pdf", 2048000, "/docs/employee-handbook.pdf", null, null, adminUserId, "Company employee handbook", "handbook,policy", true, DocumentCategory.Policy, tenantIdStr),
            Document.Create("Leave Policy 2026", "leave-policy-2026.pdf", "application/pdf", 512000, "/docs/leave-policy-2026.pdf", null, null, adminUserId, "Leave policy for 2026", "leave,policy", true, DocumentCategory.Policy, tenantIdStr),
            Document.Create("Code of Conduct", "code-of-conduct.pdf", "application/pdf", 1024000, "/docs/code-of-conduct.pdf", null, null, adminUserId, "Company code of conduct", "conduct,policy", true, DocumentCategory.Policy, tenantIdStr)
        });

        _db.Projects.Add(Project.Create(
            "HRMS Platform v2.0", "Next generation HRMS platform", "HRMS-01",
            departments[0].Id, null, now.AddDays(-90), null,
            ProjectPriority.High, 100000, "USD", null, tenantId));

        _db.ExpenseCategories.AddRange(new[]
        {
            ExpenseCategoryEntity.Create("Travel", "TRV", "Travel expenses", null, tenantIdStr),
            ExpenseCategoryEntity.Create("Meals", "MLS", "Meals and entertainment", null, tenantIdStr),
            ExpenseCategoryEntity.Create("Office Supplies", "OFC", "Office supplies", null, tenantIdStr)
        });

        _db.ChatChannels.AddRange(new[]
        {
            ChatChannel.Create("General", ChannelType.Public, adminUserId, "General discussion channel", tenantId),
            ChatChannel.Create("Engineering", ChannelType.Public, adminUserId, "Engineering team channel", tenantId),
            ChatChannel.Create("Random", ChannelType.Public, adminUserId, "Random fun stuff", tenantId)
        });

        _db.Dashboards.Add(Dashboard.Create(
            "HR Overview", "Main HR dashboard with key metrics", adminUserId, true, false, null, tenantIdStr));

        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Demo data seeded successfully for tenant {TenantId}", tenantId);
        return Ok(new
        {
            message = "Demo data seeded successfully!",
            tenantId,
            summary = new
            {
                company = company.Name,
                branches = branches.Length,
                departments = departments.Length,
                grades = grades.Length,
                designations = designations.Length,
                shifts = shifts.Length,
                employees = employeeNames.Length,
                leaveTypes = leaveTypes.Length,
                ticketCategories = 4,
                documents = 3,
                projects = 1,
                expenseCategories = 3,
                chatChannels = 3,
                dashboards = 1
            }
        });
    }
}
