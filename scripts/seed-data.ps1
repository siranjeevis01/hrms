<#
.SYNOPSIS
    HRMS Pro - Database Seed Script
.DESCRIPTION
    Seeds initial data into the HRMS Pro MySQL databases.
    Creates default admin user, roles, permissions, and sample data.
.EXAMPLE
    .\scripts\seed-data.ps1
.EXAMPLE
    .\scripts\seed-data.ps1 -Full
#>

param(
    [switch]$Full,
    [switch]$SkipBackup
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RootDir = Split-Path -Parent $ScriptDir

# ============================================
# Helper Functions
# ============================================

function Write-Header {
    param([string]$Message)
    Write-Host ""
    Write-Host "============================================" -ForegroundColor Cyan
    Write-Host "  $Message" -ForegroundColor Cyan
    Write-Host "============================================" -ForegroundColor Cyan
    Write-Host ""
}

function Write-Step {
    param([string]$Message)
    Write-Host "[STEP] $Message" -ForegroundColor Yellow
}

function Write-Success {
    param([string]$Message)
    Write-Host "[OK] $Message" -ForegroundColor Green
}

function Write-Error {
    param([string]$Message)
    Write-Host "[ERROR] $Message" -ForegroundColor Red
}

function Write-Info {
    param([string]$Message)
    Write-Host "[INFO] $Message" -ForegroundColor White
}

function Invoke-MySQLCommand {
    param(
        [string]$Database,
        [string]$Query
    )
    
    $env:MYSQL_PWD = "Hrms@123456"
    $result = docker exec hrms-mysql mysql -u hrms_admin -D $Database -e "$Query" 2>&1
    $env:MYSQL_PWD = $null
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error "MySQL command failed: $result"
        return $false
    }
    return $true
}

function Test-MySQLConnection {
    $env:MYSQL_PWD = "Hrms@123456"
    $result = docker exec hrms-mysql mysql -u hrms_admin -e "SELECT 1" 2>&1
    $env:MYSQL_PWD = $null
    
    return ($LASTEXITCODE -eq 0)
}

function Test-DatabaseExists {
    param([string]$DatabaseName)
    
    $env:MYSQL_PWD = "Hrms@123456"
    $result = docker exec hrms-mysql mysql -u hrms_admin -e "SHOW DATABASES LIKE '$DatabaseName'" 2>&1
    $env:MYSQL_PWD = $null
    
    return ($result -match $DatabaseName)
}

# ============================================
# Seed Data SQL Queries
# ============================================

$CreateRolesQuery = @"
INSERT IGNORE INTO roles (Id, Name, NormalizedName, Description, CreatedAt, IsActive) VALUES
(1, 'SuperAdmin', 'SUPERADMIN', 'Full system access with all permissions', NOW(), true),
(2, 'Admin', 'ADMIN', 'Administrative access to manage users and settings', NOW(), true),
(3, 'HRManager', 'HRMANAGER', 'Human Resources management access', NOW(), true),
(4, 'DepartmentHead', 'DEPARTMENTHEAD', 'Department head with team management access', NOW(), true),
(5, 'Manager', 'MANAGER', 'Team manager with limited administrative access', NOW(), true),
(6, 'Employee', 'EMPLOYEE', 'Standard employee access', NOW(), true),
(7, 'Intern', 'INTERN', 'Intern with restricted access', NOW(), true),
(8, 'Contractor', 'CONTRACTOR', 'External contractor with limited access', NOW(), true),
(9, 'Auditor', 'AUDITOR', 'Read-only access for auditing purposes', NOW(), true),
(10, 'PayrollOfficer', 'PAYROLLOFFICER', 'Payroll management access', NOW(), true);
"@

$CreateAdminUserQuery = @"
INSERT IGNORE INTO users (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, CreatedAt, UpdatedAt, IsActive, FirstName, LastName) VALUES
('a1b2c3d4-e5f6-7890-abcd-ef1234567890', 'admin', 'ADMIN', 'admin@hrms-pro.com', 'ADMIN@HRMS-PRO.COM', true, 'AQAAAAIAAYagAAAAENDfYu7Q0lP0lPu3mYBqKjUwE7tH8V7K9x2m3n4p5q6r7s8t9u0v1w2x3y4z5', 'STAMP-ADMIN-001', 'GUID-ADMIN-001', '+1234567890', true, false, NULL, false, 0, NOW(), NOW(), true, 'System', 'Administrator');
"@

$CreateUserRolesQuery = @"
INSERT IGNORE INTO user_roles (UserId, RoleId) VALUES
('a1b2c3d4-e5f6-7890-abcd-ef1234567890', 1);
"@

$CreateCompaniesQuery = @"
INSERT IGNORE INTO companies (Id, Name, Code, Address, City, State, Country, PostalCode, Phone, Email, Website, FoundedDate, CreatedAt, IsActive) VALUES
(1, 'HRMS Pro Corporation', 'HRMSP', '123 Business Avenue', 'New York', 'NY', 'USA', '10001', '+1-555-0100', 'info@hrms-pro.com', 'https://hrms-pro.com', '2020-01-01', NOW(), true),
(2, 'Tech Solutions Inc', 'TECHS', '456 Innovation Drive', 'San Francisco', 'CA', 'USA', '94105', '+1-555-0200', 'info@techsolutions.com', 'https://techsolutions.com', '2018-06-15', NOW(), true);
"@

$CreateDepartmentsQuery = @"
INSERT IGNORE INTO departments (Id, Name, Code, CompanyId, ManagerId, ParentDepartmentId, CreatedAt, IsActive) VALUES
(1, 'Human Resources', 'HR', 1, NULL, NULL, NOW(), true),
(2, 'Engineering', 'ENG', 1, NULL, NULL, NOW(), true),
(3, 'Finance', 'FIN', 1, NULL, NULL, NOW(), true),
(4, 'Marketing', 'MKT', 1, NULL, NULL, NOW(), true),
(5, 'Operations', 'OPS', 1, NULL, NULL, NOW(), true),
(6, 'Sales', 'SAL', 1, NULL, NULL, NOW(), true),
(7, 'Legal', 'LEG', 1, NULL, NULL, NOW(), true),
(8, 'IT Support', 'ITS', 1, NULL, NULL, NOW(), true),
(9, 'Research & Development', 'RD', 1, NULL, NULL, NOW(), true),
(10, 'Customer Service', 'CS', 1, NULL, NULL, NOW(), true),
(11, 'Frontend Team', 'FET', 1, NULL, 2, NOW(), true),
(12, 'Backend Team', 'BET', 1, NULL, 2, NOW(), true),
(13, 'DevOps Team', 'DOT', 1, NULL, 2, NOW(), true),
(14, 'QA Team', 'QAT', 1, NULL, 2, NOW(), true),
(15, 'Product Team', 'PRT', 1, NULL, 2, NOW(), true);
"@

$CreateDesignationsQuery = @"
INSERT IGNORE INTO designations (Id, Name, Code, Level, MinSalary, MaxSalary, CreatedAt, IsActive) VALUES
(1, 'CEO', 'CEO', 'C-Suite', 150000, 500000, NOW(), true),
(2, 'CTO', 'CTO', 'C-Suite', 140000, 450000, NOW(), true),
(3, 'CFO', 'CFO', 'C-Suite', 140000, 450000, NOW(), true),
(4, 'VP Engineering', 'VPE', 'VP', 120000, 350000, NOW(), true),
(5, 'VP Human Resources', 'VPHR', 'VP', 110000, 300000, NOW(), true),
(6, 'Director', 'DIR', 'Director', 100000, 250000, NOW(), true),
(7, 'Senior Manager', 'SMGR', 'Senior', 85000, 200000, NOW(), true),
(8, 'Manager', 'MGR', 'Mid', 70000, 150000, NOW(), true),
(9, 'Senior Engineer', 'SENG', 'Senior', 80000, 180000, NOW(), true),
(10, 'Engineer', 'ENG', 'Mid', 60000, 120000, NOW(), true),
(11, 'Junior Engineer', 'JENG', 'Junior', 40000, 80000, NOW(), true),
(12, 'Senior Analyst', 'SANA', 'Senior', 75000, 150000, NOW(), true),
(13, 'Analyst', 'ANA', 'Mid', 50000, 100000, NOW(), true),
(14, 'Junior Analyst', 'JANA', 'Junior', 35000, 70000, NOW(), true),
(15, 'Intern', 'INT', 'Entry', 20000, 40000, NOW(), true);
"@

$CreateLeaveTypesQuery = @"
INSERT IGNORE INTO leave_types (Id, Name, Code, Description, DefaultDays, IsPaid, IsCarryForward, MaxCarryForwardDays, IsActive, CreatedAt) VALUES
(1, 'Annual Leave', 'AL', 'Paid annual vacation leave', 20, true, true, 5, true, NOW()),
(2, 'Sick Leave', 'SL', 'Leave due to illness', 12, true, false, 0, true, NOW()),
(3, 'Personal Leave', 'PL', 'Personal time off', 5, true, false, 0, true, NOW()),
(4, 'Maternity Leave', 'ML', 'Maternity leave for new mothers', 90, true, false, 0, true, NOW()),
(5, 'Paternity Leave', 'PTL', 'Paternity leave for new fathers', 15, true, false, 0, true, NOW()),
(6, 'Bereavement Leave', 'BL', 'Leave due to death of family member', 5, true, false, 0, true, NOW()),
(7, 'Marriage Leave', 'MRGL', 'Leave for getting married', 5, true, false, 0, true, NOW()),
(8, 'Unpaid Leave', 'UL', 'Leave without pay', 0, false, false, 0, true, NOW()),
(9, 'Work From Home', 'WFH', 'Remote work day', 0, true, false, 0, true, NOW()),
(10, 'Compensatory Off', 'CO', 'Comp off for extra working days', 0, true, true, 10, true, NOW());
"@

$CreateAttendanceStatusesQuery = @"
INSERT IGNORE INTO attendance_statuses (Id, Name, Code, Color, IsActive, CreatedAt) VALUES
(1, 'Present', 'PRS', '#28a745', true, NOW()),
(2, 'Absent', 'ABS', '#dc3545', true, NOW()),
(3, 'Half Day', 'HD', '#ffc107', true, NOW()),
(4, 'Late', 'LATE', '#fd7e14', true, NOW()),
(5, 'On Leave', 'OL', '#6f42c1', true, NOW()),
(6, 'Work From Home', 'WFH', '#17a2b8', true, NOW()),
(7, 'Holiday', 'HOL', '#6c757d', true, NOW()),
(8, 'Weekend', 'WE', '#343a40', true, NOW()),
(9, 'Business Trip', 'BT', '#20c997', true, NOW()),
(10, 'Training', 'TRN', '#e83e8c', true, NOW());
"@

$CreatePermissionsQuery = @"
INSERT IGNORE INTO permissions (Id, Name, Module, Description, CreatedAt) VALUES
(1, 'users.view', 'Users', 'View user list and details', NOW()),
(2, 'users.create', 'Users', 'Create new users', NOW()),
(3, 'users.edit', 'Users', 'Edit user information', NOW()),
(4, 'users.delete', 'Users', 'Delete users', NOW()),
(5, 'employees.view', 'Employees', 'View employee list and details', NOW()),
(6, 'employees.create', 'Employees', 'Create new employee records', NOW()),
(7, 'employees.edit', 'Employees', 'Edit employee information', NOW()),
(8, 'employees.delete', 'Employees', 'Delete employee records', NOW()),
(9, 'attendance.view', 'Attendance', 'View attendance records', NOW()),
(10, 'attendance.mark', 'Attendance', 'Mark attendance', NOW()),
(11, 'attendance.edit', 'Attendance', 'Edit attendance records', NOW()),
(12, 'attendance.approve', 'Attendance', 'Approve attendance requests', NOW()),
(13, 'leave.view', 'Leave', 'View leave balances and requests', NOW()),
(14, 'leave.apply', 'Leave', 'Apply for leave', NOW()),
(15, 'leave.approve', 'Leave', 'Approve/reject leave requests', NOW()),
(16, 'leave.manage', 'Leave', 'Manage leave types and policies', NOW()),
(17, 'payroll.view', 'Payroll', 'View payroll information', NOW()),
(18, 'payroll.process', 'Payroll', 'Process payroll', NOW()),
(19, 'payroll.approve', 'Payroll', 'Approve payroll', NOW()),
(20, 'payroll.manage', 'Payroll', 'Manage salary structures', NOW()),
(21, 'recruitment.view', 'Recruitment', 'View job postings and applications', NOW()),
(22, 'recruitment.manage', 'Recruitment', 'Manage job postings', NOW()),
(23, 'recruitment.approve', 'Recruitment', 'Approve hiring decisions', NOW()),
(24, 'reports.view', 'Reports', 'View reports and analytics', NOW()),
(25, 'reports.export', 'Reports', 'Export reports', NOW()),
(26, 'settings.view', 'Settings', 'View system settings', NOW()),
(27, 'settings.manage', 'Settings', 'Manage system settings', NOW()),
(28, 'departments.view', 'Departments', 'View departments', NOW()),
(29, 'departments.manage', 'Departments', 'Manage departments', NOW()),
(30, 'notifications.manage', 'Notifications', 'Manage notification settings', NOW());
"@

$CreateHolidaysQuery = @"
INSERT IGNORE INTO holidays (Id, Name, Date, Year, IsRecurring, CreatedAt, IsActive) VALUES
(1, 'New Year''s Day', '2024-01-01', 2024, true, NOW(), true),
(2, 'Martin Luther King Jr. Day', '2024-01-15', 2024, false, NOW(), true),
(3, 'Presidents'' Day', '2024-02-19', 2024, false, NOW(), true),
(4, 'Memorial Day', '2024-05-27', 2024, false, NOW(), true),
(5, 'Independence Day', '2024-07-04', 2024, true, NOW(), true),
(6, 'Labor Day', '2024-09-02', 2024, false, NOW(), true),
(7, 'Thanksgiving Day', '2024-11-28', 2024, false, NOW(), true),
(8, 'Day After Thanksgiving', '2024-11-29', 2024, false, NOW(), true),
(9, 'Christmas Eve', '2024-12-24', 2024, true, NOW(), true),
(10, 'Christmas Day', '2024-12-25', 2024, true, NOW(), true),
(11, 'New Year''s Eve', '2024-12-31', 2024, true, NOW(), true);
"@

$CreateNotificationTemplatesQuery = @"
INSERT IGNORE INTO notification_templates (Id, Name, Type, Subject, Body, IsActive, CreatedAt) VALUES
(1, 'Welcome Email', 'Email', 'Welcome to HRMS Pro', 'Dear {{FirstName}}, Welcome to HRMS Pro! Your account has been created successfully.', true, NOW()),
(2, 'Password Reset', 'Email', 'Password Reset Request', 'Dear {{FirstName}}, You have requested a password reset. Click the link to reset your password.', true, NOW()),
(3, 'Leave Approved', 'Email', 'Leave Request Approved', 'Dear {{FirstName}}, Your leave request from {{StartDate}} to {{EndDate}} has been approved.', true, NOW()),
(4, 'Leave Rejected', 'Email', 'Leave Request Rejected', 'Dear {{FirstName}}, Your leave request from {{StartDate}} to {{EndDate}} has been rejected.', true, NOW()),
(5, 'Payroll Generated', 'Email', 'Payroll Generated', 'Dear {{FirstName}}, Your salary slip for {{Month}} {{Year}} is now available.', true, NOW()),
(6, 'Attendance Reminder', 'Push', 'Attendance Reminder', 'Please mark your attendance for today.', true, NOW()),
(7, 'Birthday Wish', 'Email', 'Happy Birthday!', 'Dear {{FirstName}}, Wishing you a very Happy Birthday!', true, NOW()),
(8, 'Work Anniversary', 'Email', 'Happy Work Anniversary!', 'Dear {{FirstName}}, Congratulations on your {{Years}} year(s) with us!', true, NOW());
"@

$CreateProjectStatusesQuery = @"
INSERT IGNORE INTO project_statuses (Id, Name, Code, Color, SortOrder, IsActive, CreatedAt) VALUES
(1, 'Planning', 'PLN', '#6c757d', 1, true, NOW()),
(2, 'In Progress', 'IPG', '#007bff', 2, true, NOW()),
(3, 'On Hold', 'OHL', '#ffc107', 3, true, NOW()),
(4, 'Completed', 'CMP', '#28a745', 4, true, NOW()),
(5, 'Cancelled', 'CNL', '#dc3545', 5, true, NOW());
"@

# ============================================
# Main Script
# ============================================

Write-Header "HRMS Pro - Database Seed Script"

# Step 1: Check MySQL is running
Write-Step "Checking MySQL connection..."
if (-not (Test-MySQLConnection)) {
    Write-Error "Cannot connect to MySQL. Make sure the containers are running."
    Write-Info "Run '.\scripts\setup.ps1' first to start the services."
    exit 1
}
Write-Success "MySQL connection successful"

# Step 2: Wait for MySQL to be fully ready
Write-Step "Waiting for MySQL to be fully ready..."
$maxRetries = 30
$retryCount = 0
while ($retryCount -lt $maxRetries) {
    $env:MYSQL_PWD = "Hrms@123456"
    $ready = docker exec hrms-mysql mysql -u hrms_admin -e "SELECT 1" 2>&1
    $env:MYSQL_PWD = $null
    
    if ($LASTEXITCODE -eq 0) {
        break
    }
    
    $retryCount++
    Write-Host "." -NoNewline
    Start-Sleep -Seconds 2
}
Write-Host ""
Write-Success "MySQL is ready"

# Step 3: Seed Identity Database
Write-Step "Seeding hrms_identity database..."

Invoke-MySQLCommand -Database "hrms_identity" -Query $CreateRolesQuery
Write-Success "Roles created"

Invoke-MySQLCommand -Database "hrms_identity" -Query $CreateAdminUserQuery
Write-Success "Admin user created"

Invoke-MySQLCommand -Database "hrms_identity" -Query $CreateUserRolesQuery
Write-Success "User roles assigned"

Invoke-MySQLCommand -Database "hrms_identity" -Query $CreatePermissionsQuery
Write-Success "Permissions created"

# Step 4: Seed Organization Database
Write-Step "Seeding hrms_organization database..."

Invoke-MySQLCommand -Database "hrms_organization" -Query $CreateCompaniesQuery
Write-Success "Companies created"

Invoke-MySQLCommand -Database "hrms_organization" -Query $CreateDepartmentsQuery
Write-Success "Departments created"

Invoke-MySQLCommand -Database "hrms_organization" -Query $CreateDesignationsQuery
Write-Success "Designations created"

# Step 5: Seed Leave Database
Write-Step "Seeding hrms_leave database..."

Invoke-MySQLCommand -Database "hrms_leave" -Query $CreateLeaveTypesQuery
Write-Success "Leave types created"

# Step 6: Seed Attendance Database
Write-Step "Seeding hrms_attendance database..."

Invoke-MySQLCommand -Database "hrms_attendance" -Query $CreateAttendanceStatusesQuery
Write-Success "Attendance statuses created"

Invoke-MySQLCommand -Database "hrms_attendance" -Query $CreateHolidaysQuery
Write-Success "Holidays created"

# Step 7: Seed Notification Database
Write-Step "Seeding hrms_notification database..."

Invoke-MySQLCommand -Database "hrms_notification" -Query $CreateNotificationTemplatesQuery
Write-Success "Notification templates created"

# Step 8: Seed Project Database (if Full flag is set)
if ($Full) {
    Write-Step "Seeding hrms_project database..."
    
    Invoke-MySQLCommand -Database "hrms_project" -Query $CreateProjectStatusesQuery
    Write-Success "Project statuses created"
    
    # Add more seed data for full mode
    $CreatePrioritiesQuery = @"
INSERT IGNORE INTO priorities (Id, Name, Code, Color, SortOrder, IsActive, CreatedAt) VALUES
(1, 'Critical', 'CRT', '#dc3545', 1, true, NOW()),
(2, 'High', 'HIGH', '#fd7e14', 2, true, NOW()),
(3, 'Medium', 'MED', '#ffc107', 3, true, NOW()),
(4, 'Low', 'LOW', '#28a745', 4, true, NOW());
"@
    
    Invoke-MySQLCommand -Database "hrms_project" -Query $CreatePrioritiesQuery
    Write-Success "Priorities created"
}

# Step 9: Create audit log tables
Write-Step "Seeding hrms_audit database..."

$CreateAuditLogQuery = @"
CREATE TABLE IF NOT EXISTS audit_logs (
    Id BIGINT AUTO_INCREMENT PRIMARY KEY,
    UserId VARCHAR(45) NULL,
    Action VARCHAR(50) NOT NULL,
    EntityType VARCHAR(100) NOT NULL,
    EntityId VARCHAR(45) NULL,
    OldValues JSON NULL,
    NewValues JSON NULL,
    IPAddress VARCHAR(45) NULL,
    UserAgent TEXT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_audit_user (UserId),
    INDEX idx_audit_entity (EntityType, EntityId),
    INDEX idx_audit_created (CreatedAt)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
"@

Invoke-MySQLCommand -Database "hrms_audit" -Query $CreateAuditLogQuery
Write-Success "Audit log table created"

# Step 10: Summary
Write-Header "Seed Complete"

Write-Host "Databases seeded:" -ForegroundColor Green
Write-Host "  hrms_identity       - Users, Roles, Permissions" -ForegroundColor White
Write-Host "  hrms_organization   - Companies, Departments, Designations" -ForegroundColor White
Write-Host "  hrms_leave          - Leave Types" -ForegroundColor White
Write-Host "  hrms_attendance     - Attendance Statuses, Holidays" -ForegroundColor White
Write-Host "  hrms_notification   - Notification Templates" -ForegroundColor White
Write-Host "  hrms_audit          - Audit Log Tables" -ForegroundColor White

if ($Full) {
    Write-Host "  hrms_project        - Project Statuses, Priorities" -ForegroundColor White
}

Write-Host ""
Write-Host "Default Admin Credentials:" -ForegroundColor Yellow
Write-Host "  Email:    admin@hrms-pro.com" -ForegroundColor White
Write-Host "  Password: Admin@123" -ForegroundColor White
Write-Host ""

Write-Host "Next Steps:" -ForegroundColor Yellow
Write-Host "  1. Access the application at http://localhost:4200" -ForegroundColor White
Write-Host "  2. Login with admin credentials" -ForegroundColor White
Write-Host "  3. Start configuring your organization" -ForegroundColor White
Write-Host ""

Write-Success "Database seeding completed successfully!"