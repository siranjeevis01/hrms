using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Audit.Application.Interfaces;
using HRMS.Services.Chat.Application.Interfaces;
using HRMS.Services.Dashboard.Application.Interfaces;
using HRMS.Services.Document.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Expense.Application.Interfaces;
using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Notification.Application.Interfaces;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Report.Application.Interfaces;
using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Workflow.Application.Interfaces;

namespace HRMS.Api;

public class EmployeeDbContextAdapter : IEmployeeDbContext
{
    private readonly HrmsDbContext _c;
    public EmployeeDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Employee.Domain.Entities.Employee> Employees => _c.Employees;
    public DbSet<HRMS.Services.Employee.Domain.Entities.EmergencyContact> EmergencyContacts => _c.EmergencyContacts;
    public DbSet<HRMS.Services.Employee.Domain.Entities.EmployeeDocument> Documents => _c.EmployeeDocuments;
    public DbSet<HRMS.Services.Employee.Domain.Entities.BankDetail> BankDetails => _c.BankDetails;
    public DbSet<HRMS.Services.Employee.Domain.Entities.Education> Educations => _c.Educations;
    public DbSet<HRMS.Services.Employee.Domain.Entities.WorkExperience> WorkExperiences => _c.WorkExperiences;
    public DbSet<HRMS.Services.Employee.Domain.Entities.Certification> Certifications => _c.Certifications;
    public DbSet<HRMS.Services.Employee.Domain.Entities.Skill> Skills => _c.Skills;
    public DbSet<HRMS.Services.Employee.Domain.Entities.SalaryStructure> SalaryStructures => _c.SalaryStructures;
    public DbSet<HRMS.Services.Employee.Domain.Entities.EmployeeHistory> EmployeeHistories => _c.EmployeeHistories;
    public DbSet<HRMS.Services.Employee.Domain.Entities.Dependent> Dependents => _c.Dependents;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class OrganizationDbContextAdapter : IOrganizationDbContext
{
    private readonly HrmsDbContext _c;
    public OrganizationDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Organization.Domain.Entities.Company> Companies => _c.Companies;
    public DbSet<HRMS.Services.Organization.Domain.Entities.Branch> Branches => _c.Branches;
    public DbSet<HRMS.Services.Organization.Domain.Entities.Department> Departments => _c.Departments;
    public DbSet<HRMS.Services.Organization.Domain.Entities.Designation> Designations => _c.Designations;
    public DbSet<HRMS.Services.Organization.Domain.Entities.Grade> Grades => _c.Grades;
    public DbSet<HRMS.Services.Organization.Domain.Entities.Shift> Shifts => _c.Shifts;
    public DbSet<HRMS.Services.Organization.Domain.Entities.Holiday> Holidays => _c.Holidays;
    public DbSet<HRMS.Services.Organization.Domain.Entities.CompanyPolicy> CompanyPolicies => _c.CompanyPolicies;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class AttendanceDbContextAdapter : IAttendanceDbContext
{
    private readonly HrmsDbContext _c;
    public AttendanceDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Attendance.Domain.Entities.AttendanceRecord> AttendanceRecords => _c.AttendanceRecords;
    public DbSet<HRMS.Services.Attendance.Domain.Entities.ShiftAssignment> ShiftAssignments => _c.ShiftAssignments;
    public DbSet<HRMS.Services.Attendance.Domain.Entities.GeoFence> GeoFences => _c.GeoFences;
    public DbSet<HRMS.Services.Attendance.Domain.Entities.WifiNetwork> WifiNetworks => _c.WifiNetworks;
    public DbSet<HRMS.Services.Attendance.Domain.Entities.AttendanceRegularization> AttendanceRegularizations => _c.AttendanceRegularizations;
    public DbSet<HRMS.Services.Attendance.Domain.Entities.WorkFromHome> WorkFromHomes => _c.WorkFromHomes;
    public DbSet<HRMS.Services.Attendance.Domain.Entities.AttendanceSummary> AttendanceSummaries => _c.AttendanceSummaries;
    public DbSet<HRMS.Services.Attendance.Domain.Entities.AttendancePolicy> AttendancePolicies => _c.AttendancePolicies;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class LeaveDbContextAdapter : ILeaveDbContext
{
    private readonly HrmsDbContext _c;
    public LeaveDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveType> LeaveTypes => _c.LeaveTypes;
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveBalance> LeaveBalances => _c.LeaveBalances;
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveApplication> LeaveApplications => _c.LeaveApplications;
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveComment> LeaveComments => _c.LeaveComments;
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveApprovalMatrix> LeaveApprovalMatrices => _c.LeaveApprovalMatrices;
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveAccrualPolicy> LeaveAccrualPolicies => _c.LeaveAccrualPolicies;
    public DbSet<HRMS.Services.Leave.Domain.Entities.CompOff> CompOffs => _c.CompOffs;
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeavePolicy> LeavePolicies => _c.LeavePolicies;
    public DbSet<HRMS.Services.Leave.Domain.Entities.HolidayCalendarEntry> HolidayCalendarEntries => _c.HolidayCalendarEntries;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class PayrollDbContextAdapter : IPayrollDbContext
{
    private readonly HrmsDbContext _c;
    public PayrollDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.PayrollRun> PayrollRuns => _c.PayrollRuns;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.EmployeePayroll> EmployeePayrolls => _c.EmployeePayrolls;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.SalaryComponentDef> SalaryComponentDefs => _c.SalaryComponentDefs;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.EmployeeSalaryAssignment> EmployeeSalaryAssignments => _c.EmployeeSalaryAssignments;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Allowance> Allowances => _c.Allowances;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Deduction> Deductions => _c.Deductions;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Bonus> Bonuses => _c.Bonuses;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Loan> Loans => _c.Loans;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.LoanRepayment> LoanRepayments => _c.LoanRepayments;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.TaxConfiguration> TaxConfigurations => _c.TaxConfigurations;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.EmployeeTaxDeclaration> EmployeeTaxDeclarations => _c.EmployeeTaxDeclarations;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Payslip> Payslips => _c.Payslips;
    public DbSet<HRMS.Services.Payroll.Domain.Entities.PayrollAuditLog> PayrollAuditLogs => _c.PayrollAuditLogs;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class NotificationDbContextAdapter : INotificationDbContext
{
    private readonly HrmsDbContext _c;
    public NotificationDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Notification.Domain.Entities.Notification> Notifications => _c.Notifications;
    public DbSet<HRMS.Services.Notification.Domain.Entities.NotificationTemplate> NotificationTemplates => _c.NotificationTemplates;
    public DbSet<HRMS.Services.Notification.Domain.Entities.NotificationPreference> NotificationPreferences => _c.NotificationPreferences;
    public DbSet<HRMS.Services.Notification.Domain.Entities.NotificationGroup> NotificationGroups => _c.NotificationGroups;
    public DbSet<HRMS.Services.Notification.Domain.Entities.NotificationDeliveryLog> NotificationDeliveryLogs => _c.NotificationDeliveryLogs;
    public DbSet<HRMS.Services.Notification.Domain.Entities.EmailQueue> EmailQueues => _c.EmailQueues;
    public DbSet<HRMS.Services.Notification.Domain.Entities.SmsQueue> SmsQueues => _c.SmsQueues;
    public DbSet<HRMS.Services.Notification.Domain.Entities.PushNotification> PushNotifications => _c.PushNotifications;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class RecruitmentDbContextAdapter : IRecruitmentDbContext
{
    private readonly HrmsDbContext _c;
    public RecruitmentDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.Candidate> Candidates => _c.Candidates;
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.JobPosting> JobPostings => _c.JobPostings;
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.JobApplication> JobApplications => _c.JobApplications;
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.Interview> Interviews => _c.Interviews;
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.InterviewFeedback> InterviewFeedbacks => _c.InterviewFeedbacks;
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.OfferLetter> OfferLetters => _c.OfferLetters;
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.OnboardingChecklist> OnboardingChecklists => _c.OnboardingChecklists;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class ProjectTaskDbContextAdapter : IProjectTaskDbContext
{
    private readonly HrmsDbContext _c;
    public ProjectTaskDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Project> Projects => _c.Projects;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.ProjectMember> ProjectMembers => _c.ProjectMembers;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Epic> Epics => _c.Epics;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Story> Stories => _c.Stories;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.TaskItem> TaskItems => _c.TaskItems;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Bug> Bugs => _c.Bugs;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Sprint> Sprints => _c.Sprints;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Board> Boards => _c.Boards;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Comment> Comments => _c.Comments;
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.TimeLog> TimeLogs => _c.TimeLogs;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class PerformanceDbContextAdapter : IPerformanceDbContext
{
    private readonly HrmsDbContext _c;
    public PerformanceDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Performance.Domain.Entities.Goal> Goals => _c.Goals;
    public DbSet<HRMS.Services.Performance.Domain.Entities.KeyResult> KeyResults => _c.KeyResults;
    public DbSet<HRMS.Services.Performance.Domain.Entities.OKR> OKRs => _c.OKRs;
    public DbSet<HRMS.Services.Performance.Domain.Entities.OKRItem> OKRItems => _c.OKRItems;
    public DbSet<HRMS.Services.Performance.Domain.Entities.KPI> KPIs => _c.KPIs;
    public DbSet<HRMS.Services.Performance.Domain.Entities.PerformanceReview> PerformanceReviews => _c.PerformanceReviews;
    public DbSet<HRMS.Services.Performance.Domain.Entities.ReviewCriteria> ReviewCriteria => _c.ReviewCriterias;
    public DbSet<HRMS.Services.Performance.Domain.Entities.Feedback360> Feedback360s => _c.Feedback360s;
    public DbSet<HRMS.Services.Performance.Domain.Entities.FeedbackAnswer> FeedbackAnswers => _c.FeedbackAnswers;
    public DbSet<HRMS.Services.Performance.Domain.Entities.CalibrationSession> CalibrationSessions => _c.CalibrationSessions;
    public DbSet<HRMS.Services.Performance.Domain.Entities.CalibrationEntry> CalibrationEntries => _c.CalibrationEntries;
    public DbSet<HRMS.Services.Performance.Domain.Entities.Appraisal> Appraisals => _c.Appraisals;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class TrainingDbContextAdapter : ITrainingDbContext
{
    private readonly HrmsDbContext _c;
    public TrainingDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Training.Domain.Entities.Course> Courses => _c.Courses;
    public DbSet<HRMS.Services.Training.Domain.Entities.CourseModule> CourseModules => _c.CourseModules;
    public DbSet<HRMS.Services.Training.Domain.Entities.Lesson> Lessons => _c.Lessons;
    public DbSet<HRMS.Services.Training.Domain.Entities.Enrollment> Enrollments => _c.Enrollments;
    public DbSet<HRMS.Services.Training.Domain.Entities.LessonProgress> LessonProgresses => _c.LessonProgresses;
    public DbSet<HRMS.Services.Training.Domain.Entities.Assessment> Assessments => _c.Assessments;
    public DbSet<HRMS.Services.Training.Domain.Entities.AssessmentQuestion> AssessmentQuestions => _c.AssessmentQuestions;
    public DbSet<HRMS.Services.Training.Domain.Entities.AssessmentAttempt> AssessmentAttempts => _c.AssessmentAttempts;
    public DbSet<HRMS.Services.Training.Domain.Entities.Certificate> Certificates => _c.Certificates;
    public DbSet<HRMS.Services.Training.Domain.Entities.LearningPath> LearningPaths => _c.LearningPaths;
    public DbSet<HRMS.Services.Training.Domain.Entities.LearningPathCourse> LearningPathCourses => _c.LearningPathCourses;
    public DbSet<HRMS.Services.Training.Domain.Entities.TrainingSchedule> TrainingSchedules => _c.TrainingSchedules;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class AuditDbContextAdapter : IAuditDbContext
{
    private readonly HrmsDbContext _c;
    public AuditDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Audit.Domain.Entities.AuditLog> AuditLogs => _c.SystemAuditLogs;
    public DbSet<HRMS.Services.Audit.Domain.Entities.AuditTrail> AuditTrails => _c.AuditTrails;
    public DbSet<HRMS.Services.Audit.Domain.Entities.LoginHistory> LoginHistories => _c.LoginHistories;
    public DbSet<HRMS.Services.Audit.Domain.Entities.DataChangeLog> DataChangeLogs => _c.DataChangeLogs;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class ReportDbContextAdapter : IReportDbContext
{
    private readonly HrmsDbContext _c;
    public ReportDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Report.Domain.Entities.ReportTemplate> ReportTemplates => _c.ReportTemplates;
    public DbSet<HRMS.Services.Report.Domain.Entities.ReportInstance> ReportInstances => _c.ReportInstances;
    public DbSet<HRMS.Services.Report.Domain.Entities.ScheduledReport> ScheduledReports => _c.ScheduledReports;
    public DbSet<HRMS.Services.Report.Domain.Entities.ReportAccess> ReportAccesses => _c.ReportAccesses;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class DashboardDbContextAdapter : IDashboardDbContext
{
    private readonly HrmsDbContext _c;
    public DashboardDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.Dashboard> Dashboards => _c.Dashboards;
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.DashboardWidget> DashboardWidgets => _c.DashboardWidgets;
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.DashboardShare> DashboardShares => _c.DashboardShares;
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.WidgetPreset> WidgetPresets => _c.WidgetPresets;
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.AnalyticsEvent> AnalyticsEvents => _c.AnalyticsEvents;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class ExpenseDbContextAdapter : IExpenseDbContext
{
    private readonly HrmsDbContext _c;
    public ExpenseDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpenseClaim> ExpenseClaims => _c.ExpenseClaims;
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpenseItem> ExpenseItems => _c.ExpenseItems;
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpensePolicy> ExpensePolicies => _c.ExpensePolicies;
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpenseCategoryEntity> ExpenseCategories => _c.ExpenseCategories;
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpenseApproval> ExpenseApprovals => _c.ExpenseApprovals;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class TravelDbContextAdapter : ITravelDbContext
{
    private readonly HrmsDbContext _c;
    public TravelDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Travel.Domain.Entities.TravelRequest> TravelRequests => _c.TravelRequests;
    public DbSet<HRMS.Services.Travel.Domain.Entities.TravelItinerary> TravelItineraries => _c.TravelItineraries;
    public DbSet<HRMS.Services.Travel.Domain.Entities.TravelExpense> TravelExpenses => _c.TravelExpenses;
    public DbSet<HRMS.Services.Travel.Domain.Entities.TravelApproval> TravelApprovals => _c.TravelApprovals;
    public DbSet<HRMS.Services.Travel.Domain.Entities.VisaRequest> VisaRequests => _c.VisaRequests;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class HelpdeskDbContextAdapter : IHelpdeskDbContext
{
    private readonly HrmsDbContext _c;
    public HelpdeskDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.HelpdeskTicket> HelpdeskTickets => _c.HelpdeskTickets;
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.TicketComment> TicketComments => _c.TicketComments;
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.TicketAttachment> TicketAttachments => _c.TicketAttachments;
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.TicketCategoryEntity> TicketCategories => _c.TicketCategories;
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.KnowledgeArticle> KnowledgeArticles => _c.KnowledgeArticles;
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.Faq> Faqs => _c.Faqs;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class ChatDbContextAdapter : IChatDbContext
{
    private readonly HrmsDbContext _c;
    public ChatDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Chat.Domain.Entities.Conversation> Conversations => _c.Conversations;
    public DbSet<HRMS.Services.Chat.Domain.Entities.ConversationParticipant> ConversationParticipants => _c.ConversationParticipants;
    public DbSet<HRMS.Services.Chat.Domain.Entities.Message> Messages => _c.Messages;
    public DbSet<HRMS.Services.Chat.Domain.Entities.MessageRead> MessageReads => _c.MessageReads;
    public DbSet<HRMS.Services.Chat.Domain.Entities.MessageReaction> MessageReactions => _c.MessageReactions;
    public DbSet<HRMS.Services.Chat.Domain.Entities.ChatChannel> ChatChannels => _c.ChatChannels;
    public DbSet<HRMS.Services.Chat.Domain.Entities.ChatNotification> ChatNotifications => _c.ChatNotifications;
    public DbSet<HRMS.Services.Chat.Domain.Entities.UserPresence> UserPresences => _c.UserPresences;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class DocumentDbContextAdapter : IDocumentDbContext
{
    private readonly HrmsDbContext _c;
    public DocumentDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Document.Domain.Entities.Document> Documents => _c.Documents;
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentFolder> DocumentFolders => _c.DocumentFolders;
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentVersion> DocumentVersions => _c.DocumentVersions;
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentShare> DocumentShares => _c.DocumentShares;
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentAccessLog> DocumentAccessLogs => _c.DocumentAccessLogs;
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentTemplate> DocumentTemplates => _c.DocumentTemplates;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}

public class WorkflowDbContextAdapter : IWorkflowDbContext
{
    private readonly HrmsDbContext _c;
    public WorkflowDbContextAdapter(HrmsDbContext c) => _c = c;
    public DbSet<HRMS.Services.Workflow.Domain.Entities.WorkflowDefinition> WorkflowDefinitions => _c.WorkflowDefinitions;
    public DbSet<HRMS.Services.Workflow.Domain.Entities.WorkflowStep> WorkflowSteps => _c.WorkflowSteps;
    public DbSet<HRMS.Services.Workflow.Domain.Entities.WorkflowInstance> WorkflowInstances => _c.WorkflowInstances;
    public DbSet<HRMS.Services.Workflow.Domain.Entities.WorkflowAction> WorkflowActions => _c.WorkflowActions;
    public DbSet<HRMS.Services.Workflow.Domain.Entities.ApprovalMatrix> ApprovalMatrices => _c.ApprovalMatrices;
    public DbSet<HRMS.Services.Workflow.Domain.Entities.Delegation> Delegates => _c.Delegations;
    public DbSet<HRMS.Services.Workflow.Domain.Entities.NotificationRule> NotificationRules => _c.NotificationRules;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _c.SaveChangesAsync(ct);
}
