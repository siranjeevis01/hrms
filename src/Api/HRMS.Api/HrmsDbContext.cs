using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

using IdentityAuditLog = HRMS.Services.Identity.Domain.Entities.AuditLog;
using IdentityRefreshToken = HRMS.Services.Identity.Domain.Entities.RefreshToken;
using IdentityRole = HRMS.Services.Identity.Domain.Entities.Role;
using IdentityRolePermission = HRMS.Services.Identity.Domain.Entities.RolePermission;
using IdentityUserPermission = HRMS.Services.Identity.Domain.Entities.UserPermission;
using IdentityUserRole = HRMS.Services.Identity.Domain.Entities.UserRole;
using IdentityUserSession = HRMS.Services.Identity.Domain.Entities.UserSession;
using IdentityApplicationUser = HRMS.Services.Identity.Domain.Entities.ApplicationUser;

using SystemAuditLog = HRMS.Services.Audit.Domain.Entities.AuditLog;

namespace HRMS.Api;

public class HrmsDbContext : DbContext
{
    public HrmsDbContext(DbContextOptions<HrmsDbContext> options) : base(options) { }

    #region Identity
    public DbSet<IdentityApplicationUser> ApplicationUsers => Set<IdentityApplicationUser>();
    public DbSet<IdentityRole> Roles => Set<IdentityRole>();
    public DbSet<IdentityUserRole> UserRoles => Set<IdentityUserRole>();
    public DbSet<IdentityRolePermission> RolePermissions => Set<IdentityRolePermission>();
    public DbSet<IdentityUserPermission> UserPermissions => Set<IdentityUserPermission>();
    public DbSet<IdentityRefreshToken> RefreshTokens => Set<IdentityRefreshToken>();
    public DbSet<IdentityUserSession> UserSessions => Set<IdentityUserSession>();
    public DbSet<IdentityAuditLog> AuthAuditLogs => Set<IdentityAuditLog>();
    #endregion

    #region Organization
    public DbSet<HRMS.Services.Organization.Domain.Entities.Company> Companies => Set<HRMS.Services.Organization.Domain.Entities.Company>();
    public DbSet<HRMS.Services.Organization.Domain.Entities.CompanyPolicy> CompanyPolicies => Set<HRMS.Services.Organization.Domain.Entities.CompanyPolicy>();
    public DbSet<HRMS.Services.Organization.Domain.Entities.Department> Departments => Set<HRMS.Services.Organization.Domain.Entities.Department>();
    public DbSet<HRMS.Services.Organization.Domain.Entities.Designation> Designations => Set<HRMS.Services.Organization.Domain.Entities.Designation>();
    public DbSet<HRMS.Services.Organization.Domain.Entities.Grade> Grades => Set<HRMS.Services.Organization.Domain.Entities.Grade>();
    public DbSet<HRMS.Services.Organization.Domain.Entities.Branch> Branches => Set<HRMS.Services.Organization.Domain.Entities.Branch>();
    public DbSet<HRMS.Services.Organization.Domain.Entities.Holiday> Holidays => Set<HRMS.Services.Organization.Domain.Entities.Holiday>();
    public DbSet<HRMS.Services.Organization.Domain.Entities.Shift> Shifts => Set<HRMS.Services.Organization.Domain.Entities.Shift>();
    #endregion

    #region Employee
    public DbSet<HRMS.Services.Employee.Domain.Entities.Employee> Employees => Set<HRMS.Services.Employee.Domain.Entities.Employee>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.Education> Educations => Set<HRMS.Services.Employee.Domain.Entities.Education>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.Skill> Skills => Set<HRMS.Services.Employee.Domain.Entities.Skill>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.WorkExperience> WorkExperiences => Set<HRMS.Services.Employee.Domain.Entities.WorkExperience>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.Certification> Certifications => Set<HRMS.Services.Employee.Domain.Entities.Certification>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.Dependent> Dependents => Set<HRMS.Services.Employee.Domain.Entities.Dependent>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.EmergencyContact> EmergencyContacts => Set<HRMS.Services.Employee.Domain.Entities.EmergencyContact>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.BankDetail> BankDetails => Set<HRMS.Services.Employee.Domain.Entities.BankDetail>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.EmployeeDocument> EmployeeDocuments => Set<HRMS.Services.Employee.Domain.Entities.EmployeeDocument>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.EmployeeHistory> EmployeeHistories => Set<HRMS.Services.Employee.Domain.Entities.EmployeeHistory>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.SalaryStructure> SalaryStructures => Set<HRMS.Services.Employee.Domain.Entities.SalaryStructure>();
    public DbSet<HRMS.Services.Employee.Domain.Entities.SalaryComponent> SalaryComponents => Set<HRMS.Services.Employee.Domain.Entities.SalaryComponent>();
    #endregion

    #region Attendance
    public DbSet<HRMS.Services.Attendance.Domain.Entities.AttendanceRecord> AttendanceRecords => Set<HRMS.Services.Attendance.Domain.Entities.AttendanceRecord>();
    public DbSet<HRMS.Services.Attendance.Domain.Entities.AttendanceSummary> AttendanceSummaries => Set<HRMS.Services.Attendance.Domain.Entities.AttendanceSummary>();
    public DbSet<HRMS.Services.Attendance.Domain.Entities.AttendanceRegularization> AttendanceRegularizations => Set<HRMS.Services.Attendance.Domain.Entities.AttendanceRegularization>();
    public DbSet<HRMS.Services.Attendance.Domain.Entities.AttendancePolicy> AttendancePolicies => Set<HRMS.Services.Attendance.Domain.Entities.AttendancePolicy>();
    public DbSet<HRMS.Services.Attendance.Domain.Entities.ShiftAssignment> ShiftAssignments => Set<HRMS.Services.Attendance.Domain.Entities.ShiftAssignment>();
    public DbSet<HRMS.Services.Attendance.Domain.Entities.WorkFromHome> WorkFromHomes => Set<HRMS.Services.Attendance.Domain.Entities.WorkFromHome>();
    public DbSet<HRMS.Services.Attendance.Domain.Entities.GeoFence> GeoFences => Set<HRMS.Services.Attendance.Domain.Entities.GeoFence>();
    public DbSet<HRMS.Services.Attendance.Domain.Entities.WifiNetwork> WifiNetworks => Set<HRMS.Services.Attendance.Domain.Entities.WifiNetwork>();
    #endregion

    #region Leave
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveType> LeaveTypes => Set<HRMS.Services.Leave.Domain.Entities.LeaveType>();
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeavePolicy> LeavePolicies => Set<HRMS.Services.Leave.Domain.Entities.LeavePolicy>();
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveApplication> LeaveApplications => Set<HRMS.Services.Leave.Domain.Entities.LeaveApplication>();
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveBalance> LeaveBalances => Set<HRMS.Services.Leave.Domain.Entities.LeaveBalance>();
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveApprovalMatrix> LeaveApprovalMatrices => Set<HRMS.Services.Leave.Domain.Entities.LeaveApprovalMatrix>();
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveAccrualPolicy> LeaveAccrualPolicies => Set<HRMS.Services.Leave.Domain.Entities.LeaveAccrualPolicy>();
    public DbSet<HRMS.Services.Leave.Domain.Entities.LeaveComment> LeaveComments => Set<HRMS.Services.Leave.Domain.Entities.LeaveComment>();
    public DbSet<HRMS.Services.Leave.Domain.Entities.HolidayCalendarEntry> HolidayCalendarEntries => Set<HRMS.Services.Leave.Domain.Entities.HolidayCalendarEntry>();
    public DbSet<HRMS.Services.Leave.Domain.Entities.CompOff> CompOffs => Set<HRMS.Services.Leave.Domain.Entities.CompOff>();
    #endregion

    #region Payroll
    public DbSet<HRMS.Services.Payroll.Domain.Entities.EmployeePayroll> EmployeePayrolls => Set<HRMS.Services.Payroll.Domain.Entities.EmployeePayroll>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.EmployeeSalaryAssignment> EmployeeSalaryAssignments => Set<HRMS.Services.Payroll.Domain.Entities.EmployeeSalaryAssignment>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.PayrollRun> PayrollRuns => Set<HRMS.Services.Payroll.Domain.Entities.PayrollRun>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Payslip> Payslips => Set<HRMS.Services.Payroll.Domain.Entities.Payslip>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Allowance> Allowances => Set<HRMS.Services.Payroll.Domain.Entities.Allowance>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Deduction> Deductions => Set<HRMS.Services.Payroll.Domain.Entities.Deduction>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Bonus> Bonuses => Set<HRMS.Services.Payroll.Domain.Entities.Bonus>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.Loan> Loans => Set<HRMS.Services.Payroll.Domain.Entities.Loan>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.LoanRepayment> LoanRepayments => Set<HRMS.Services.Payroll.Domain.Entities.LoanRepayment>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.SalaryComponentDef> SalaryComponentDefs => Set<HRMS.Services.Payroll.Domain.Entities.SalaryComponentDef>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.TaxConfiguration> TaxConfigurations => Set<HRMS.Services.Payroll.Domain.Entities.TaxConfiguration>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.TaxSlab> TaxSlabs => Set<HRMS.Services.Payroll.Domain.Entities.TaxSlab>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.EmployeeTaxDeclaration> EmployeeTaxDeclarations => Set<HRMS.Services.Payroll.Domain.Entities.EmployeeTaxDeclaration>();
    public DbSet<HRMS.Services.Payroll.Domain.Entities.PayrollAuditLog> PayrollAuditLogs => Set<HRMS.Services.Payroll.Domain.Entities.PayrollAuditLog>();
    #endregion

    #region Notification
    public DbSet<HRMS.Services.Notification.Domain.Entities.Notification> Notifications => Set<HRMS.Services.Notification.Domain.Entities.Notification>();
    public DbSet<HRMS.Services.Notification.Domain.Entities.NotificationTemplate> NotificationTemplates => Set<HRMS.Services.Notification.Domain.Entities.NotificationTemplate>();
    public DbSet<HRMS.Services.Notification.Domain.Entities.NotificationPreference> NotificationPreferences => Set<HRMS.Services.Notification.Domain.Entities.NotificationPreference>();
    public DbSet<HRMS.Services.Notification.Domain.Entities.NotificationGroup> NotificationGroups => Set<HRMS.Services.Notification.Domain.Entities.NotificationGroup>();
    public DbSet<HRMS.Services.Notification.Domain.Entities.NotificationDeliveryLog> NotificationDeliveryLogs => Set<HRMS.Services.Notification.Domain.Entities.NotificationDeliveryLog>();
    public DbSet<HRMS.Services.Notification.Domain.Entities.EmailQueue> EmailQueues => Set<HRMS.Services.Notification.Domain.Entities.EmailQueue>();
    public DbSet<HRMS.Services.Notification.Domain.Entities.SmsQueue> SmsQueues => Set<HRMS.Services.Notification.Domain.Entities.SmsQueue>();
    public DbSet<HRMS.Services.Notification.Domain.Entities.PushNotification> PushNotifications => Set<HRMS.Services.Notification.Domain.Entities.PushNotification>();
    #endregion

    #region Recruitment
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.Candidate> Candidates => Set<HRMS.Services.Recruitment.Domain.Entities.Candidate>();
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.JobPosting> JobPostings => Set<HRMS.Services.Recruitment.Domain.Entities.JobPosting>();
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.JobApplication> JobApplications => Set<HRMS.Services.Recruitment.Domain.Entities.JobApplication>();
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.Interview> Interviews => Set<HRMS.Services.Recruitment.Domain.Entities.Interview>();
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.InterviewFeedback> InterviewFeedbacks => Set<HRMS.Services.Recruitment.Domain.Entities.InterviewFeedback>();
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.OfferLetter> OfferLetters => Set<HRMS.Services.Recruitment.Domain.Entities.OfferLetter>();
    public DbSet<HRMS.Services.Recruitment.Domain.Entities.OnboardingChecklist> OnboardingChecklists => Set<HRMS.Services.Recruitment.Domain.Entities.OnboardingChecklist>();
    #endregion

    #region ProjectTask
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Project> Projects => Set<HRMS.Services.ProjectTask.Domain.Entities.Project>();
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.ProjectMember> ProjectMembers => Set<HRMS.Services.ProjectTask.Domain.Entities.ProjectMember>();
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Board> Boards => Set<HRMS.Services.ProjectTask.Domain.Entities.Board>();
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Epic> Epics => Set<HRMS.Services.ProjectTask.Domain.Entities.Epic>();
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Story> Stories => Set<HRMS.Services.ProjectTask.Domain.Entities.Story>();
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.TaskItem> TaskItems => Set<HRMS.Services.ProjectTask.Domain.Entities.TaskItem>();
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Bug> Bugs => Set<HRMS.Services.ProjectTask.Domain.Entities.Bug>();
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Sprint> Sprints => Set<HRMS.Services.ProjectTask.Domain.Entities.Sprint>();
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.Comment> Comments => Set<HRMS.Services.ProjectTask.Domain.Entities.Comment>();
    public DbSet<HRMS.Services.ProjectTask.Domain.Entities.TimeLog> TimeLogs => Set<HRMS.Services.ProjectTask.Domain.Entities.TimeLog>();
    #endregion

    #region Performance
    public DbSet<HRMS.Services.Performance.Domain.Entities.PerformanceReview> PerformanceReviews => Set<HRMS.Services.Performance.Domain.Entities.PerformanceReview>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.ReviewCriteria> ReviewCriterias => Set<HRMS.Services.Performance.Domain.Entities.ReviewCriteria>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.Goal> Goals => Set<HRMS.Services.Performance.Domain.Entities.Goal>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.KPI> KPIs => Set<HRMS.Services.Performance.Domain.Entities.KPI>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.OKR> OKRs => Set<HRMS.Services.Performance.Domain.Entities.OKR>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.OKRItem> OKRItems => Set<HRMS.Services.Performance.Domain.Entities.OKRItem>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.KeyResult> KeyResults => Set<HRMS.Services.Performance.Domain.Entities.KeyResult>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.Appraisal> Appraisals => Set<HRMS.Services.Performance.Domain.Entities.Appraisal>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.Feedback360> Feedback360s => Set<HRMS.Services.Performance.Domain.Entities.Feedback360>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.FeedbackAnswer> FeedbackAnswers => Set<HRMS.Services.Performance.Domain.Entities.FeedbackAnswer>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.CalibrationSession> CalibrationSessions => Set<HRMS.Services.Performance.Domain.Entities.CalibrationSession>();
    public DbSet<HRMS.Services.Performance.Domain.Entities.CalibrationEntry> CalibrationEntries => Set<HRMS.Services.Performance.Domain.Entities.CalibrationEntry>();
    #endregion

    #region Training
    public DbSet<HRMS.Services.Training.Domain.Entities.Course> Courses => Set<HRMS.Services.Training.Domain.Entities.Course>();
    public DbSet<HRMS.Services.Training.Domain.Entities.CourseModule> CourseModules => Set<HRMS.Services.Training.Domain.Entities.CourseModule>();
    public DbSet<HRMS.Services.Training.Domain.Entities.Lesson> Lessons => Set<HRMS.Services.Training.Domain.Entities.Lesson>();
    public DbSet<HRMS.Services.Training.Domain.Entities.LessonProgress> LessonProgresses => Set<HRMS.Services.Training.Domain.Entities.LessonProgress>();
    public DbSet<HRMS.Services.Training.Domain.Entities.Enrollment> Enrollments => Set<HRMS.Services.Training.Domain.Entities.Enrollment>();
    public DbSet<HRMS.Services.Training.Domain.Entities.Certificate> Certificates => Set<HRMS.Services.Training.Domain.Entities.Certificate>();
    public DbSet<HRMS.Services.Training.Domain.Entities.LearningPath> LearningPaths => Set<HRMS.Services.Training.Domain.Entities.LearningPath>();
    public DbSet<HRMS.Services.Training.Domain.Entities.LearningPathCourse> LearningPathCourses => Set<HRMS.Services.Training.Domain.Entities.LearningPathCourse>();
    public DbSet<HRMS.Services.Training.Domain.Entities.Assessment> Assessments => Set<HRMS.Services.Training.Domain.Entities.Assessment>();
    public DbSet<HRMS.Services.Training.Domain.Entities.AssessmentQuestion> AssessmentQuestions => Set<HRMS.Services.Training.Domain.Entities.AssessmentQuestion>();
    public DbSet<HRMS.Services.Training.Domain.Entities.AssessmentAttempt> AssessmentAttempts => Set<HRMS.Services.Training.Domain.Entities.AssessmentAttempt>();
    public DbSet<HRMS.Services.Training.Domain.Entities.TrainingSchedule> TrainingSchedules => Set<HRMS.Services.Training.Domain.Entities.TrainingSchedule>();
    #endregion

    #region Audit
    public DbSet<SystemAuditLog> SystemAuditLogs => Set<SystemAuditLog>();
    public DbSet<HRMS.Services.Audit.Domain.Entities.AuditTrail> AuditTrails => Set<HRMS.Services.Audit.Domain.Entities.AuditTrail>();
    public DbSet<HRMS.Services.Audit.Domain.Entities.DataChangeLog> DataChangeLogs => Set<HRMS.Services.Audit.Domain.Entities.DataChangeLog>();
    public DbSet<HRMS.Services.Audit.Domain.Entities.LoginHistory> LoginHistories => Set<HRMS.Services.Audit.Domain.Entities.LoginHistory>();
    #endregion

    #region Report
    public DbSet<HRMS.Services.Report.Domain.Entities.ReportTemplate> ReportTemplates => Set<HRMS.Services.Report.Domain.Entities.ReportTemplate>();
    public DbSet<HRMS.Services.Report.Domain.Entities.ReportInstance> ReportInstances => Set<HRMS.Services.Report.Domain.Entities.ReportInstance>();
    public DbSet<HRMS.Services.Report.Domain.Entities.ScheduledReport> ScheduledReports => Set<HRMS.Services.Report.Domain.Entities.ScheduledReport>();
    public DbSet<HRMS.Services.Report.Domain.Entities.ReportAccess> ReportAccesses => Set<HRMS.Services.Report.Domain.Entities.ReportAccess>();
    #endregion

    #region Dashboard
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.Dashboard> Dashboards => Set<HRMS.Services.Dashboard.Domain.Entities.Dashboard>();
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.DashboardWidget> DashboardWidgets => Set<HRMS.Services.Dashboard.Domain.Entities.DashboardWidget>();
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.DashboardShare> DashboardShares => Set<HRMS.Services.Dashboard.Domain.Entities.DashboardShare>();
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.WidgetPreset> WidgetPresets => Set<HRMS.Services.Dashboard.Domain.Entities.WidgetPreset>();
    public DbSet<HRMS.Services.Dashboard.Domain.Entities.AnalyticsEvent> AnalyticsEvents => Set<HRMS.Services.Dashboard.Domain.Entities.AnalyticsEvent>();
    #endregion

    #region Expense
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpenseClaim> ExpenseClaims => Set<HRMS.Services.Expense.Domain.Entities.ExpenseClaim>();
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpenseItem> ExpenseItems => Set<HRMS.Services.Expense.Domain.Entities.ExpenseItem>();
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpenseApproval> ExpenseApprovals => Set<HRMS.Services.Expense.Domain.Entities.ExpenseApproval>();
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpensePolicy> ExpensePolicies => Set<HRMS.Services.Expense.Domain.Entities.ExpensePolicy>();
    public DbSet<HRMS.Services.Expense.Domain.Entities.ExpenseCategoryEntity> ExpenseCategories => Set<HRMS.Services.Expense.Domain.Entities.ExpenseCategoryEntity>();
    #endregion

    #region Travel
    public DbSet<HRMS.Services.Travel.Domain.Entities.TravelRequest> TravelRequests => Set<HRMS.Services.Travel.Domain.Entities.TravelRequest>();
    public DbSet<HRMS.Services.Travel.Domain.Entities.TravelItinerary> TravelItineraries => Set<HRMS.Services.Travel.Domain.Entities.TravelItinerary>();
    public DbSet<HRMS.Services.Travel.Domain.Entities.TravelExpense> TravelExpenses => Set<HRMS.Services.Travel.Domain.Entities.TravelExpense>();
    public DbSet<HRMS.Services.Travel.Domain.Entities.TravelApproval> TravelApprovals => Set<HRMS.Services.Travel.Domain.Entities.TravelApproval>();
    public DbSet<HRMS.Services.Travel.Domain.Entities.VisaRequest> VisaRequests => Set<HRMS.Services.Travel.Domain.Entities.VisaRequest>();
    #endregion

    #region Helpdesk
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.HelpdeskTicket> HelpdeskTickets => Set<HRMS.Services.Helpdesk.Domain.Entities.HelpdeskTicket>();
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.TicketComment> TicketComments => Set<HRMS.Services.Helpdesk.Domain.Entities.TicketComment>();
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.TicketAttachment> TicketAttachments => Set<HRMS.Services.Helpdesk.Domain.Entities.TicketAttachment>();
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.TicketCategoryEntity> TicketCategories => Set<HRMS.Services.Helpdesk.Domain.Entities.TicketCategoryEntity>();
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.KnowledgeArticle> KnowledgeArticles => Set<HRMS.Services.Helpdesk.Domain.Entities.KnowledgeArticle>();
    public DbSet<HRMS.Services.Helpdesk.Domain.Entities.Faq> Faqs => Set<HRMS.Services.Helpdesk.Domain.Entities.Faq>();
    #endregion

    #region Chat
    public DbSet<HRMS.Services.Chat.Domain.Entities.Conversation> Conversations => Set<HRMS.Services.Chat.Domain.Entities.Conversation>();
    public DbSet<HRMS.Services.Chat.Domain.Entities.ConversationParticipant> ConversationParticipants => Set<HRMS.Services.Chat.Domain.Entities.ConversationParticipant>();
    public DbSet<HRMS.Services.Chat.Domain.Entities.Message> Messages => Set<HRMS.Services.Chat.Domain.Entities.Message>();
    public DbSet<HRMS.Services.Chat.Domain.Entities.MessageRead> MessageReads => Set<HRMS.Services.Chat.Domain.Entities.MessageRead>();
    public DbSet<HRMS.Services.Chat.Domain.Entities.MessageReaction> MessageReactions => Set<HRMS.Services.Chat.Domain.Entities.MessageReaction>();
    public DbSet<HRMS.Services.Chat.Domain.Entities.ChatChannel> ChatChannels => Set<HRMS.Services.Chat.Domain.Entities.ChatChannel>();
    public DbSet<HRMS.Services.Chat.Domain.Entities.ChatNotification> ChatNotifications => Set<HRMS.Services.Chat.Domain.Entities.ChatNotification>();
    public DbSet<HRMS.Services.Chat.Domain.Entities.UserPresence> UserPresences => Set<HRMS.Services.Chat.Domain.Entities.UserPresence>();
    #endregion

    #region Document
    public DbSet<HRMS.Services.Document.Domain.Entities.Document> Documents => Set<HRMS.Services.Document.Domain.Entities.Document>();
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentVersion> DocumentVersions => Set<HRMS.Services.Document.Domain.Entities.DocumentVersion>();
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentFolder> DocumentFolders => Set<HRMS.Services.Document.Domain.Entities.DocumentFolder>();
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentShare> DocumentShares => Set<HRMS.Services.Document.Domain.Entities.DocumentShare>();
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentTemplate> DocumentTemplates => Set<HRMS.Services.Document.Domain.Entities.DocumentTemplate>();
    public DbSet<HRMS.Services.Document.Domain.Entities.DocumentAccessLog> DocumentAccessLogs => Set<HRMS.Services.Document.Domain.Entities.DocumentAccessLog>();
    #endregion

    #region Workflow
    public DbSet<HRMS.Services.Workflow.Domain.Entities.WorkflowDefinition> WorkflowDefinitions => Set<HRMS.Services.Workflow.Domain.Entities.WorkflowDefinition>();
    public DbSet<HRMS.Services.Workflow.Domain.Entities.WorkflowInstance> WorkflowInstances => Set<HRMS.Services.Workflow.Domain.Entities.WorkflowInstance>();
    public DbSet<HRMS.Services.Workflow.Domain.Entities.WorkflowStep> WorkflowSteps => Set<HRMS.Services.Workflow.Domain.Entities.WorkflowStep>();
    public DbSet<HRMS.Services.Workflow.Domain.Entities.WorkflowAction> WorkflowActions => Set<HRMS.Services.Workflow.Domain.Entities.WorkflowAction>();
    public DbSet<HRMS.Services.Workflow.Domain.Entities.ApprovalMatrix> ApprovalMatrices => Set<HRMS.Services.Workflow.Domain.Entities.ApprovalMatrix>();
    public DbSet<HRMS.Services.Workflow.Domain.Entities.Delegation> Delegations => Set<HRMS.Services.Workflow.Domain.Entities.Delegation>();
    public DbSet<HRMS.Services.Workflow.Domain.Entities.NotificationRule> NotificationRules => Set<HRMS.Services.Workflow.Domain.Entities.NotificationRule>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Employee.Domain.Entities.Employee).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Organization.Domain.Entities.Company).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Attendance.Domain.Entities.AttendanceRecord).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Leave.Domain.Entities.LeaveType).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Payroll.Domain.Entities.PayrollRun).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Notification.Domain.Entities.Notification).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Recruitment.Domain.Entities.Candidate).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.ProjectTask.Domain.Entities.Project).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Performance.Domain.Entities.PerformanceReview).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Training.Domain.Entities.Course).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Identity.Domain.Entities.ApplicationUser).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Audit.Domain.Entities.AuditTrail).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Report.Domain.Entities.ReportTemplate).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Dashboard.Domain.Entities.Dashboard).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Expense.Domain.Entities.ExpenseClaim).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Travel.Domain.Entities.TravelRequest).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Helpdesk.Domain.Entities.HelpdeskTicket).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Chat.Domain.Entities.Conversation).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Document.Domain.Entities.Document).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMS.Services.Workflow.Domain.Entities.WorkflowDefinition).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasIndex(nameof(BaseEntity.TenantId));
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<AggregateRoot>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
