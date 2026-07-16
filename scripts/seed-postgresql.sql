-- ============================================
-- HRMS Pro - PostgreSQL Seed Script
-- ============================================
-- For Supabase free tier (runs once on first deploy)
-- ============================================

-- Roles
INSERT INTO "Roles" ("Id", "Name", "NormalizedName", "Description", "CreatedAt", "Status")
VALUES
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567801', 'SuperAdmin', 'SUPERADMIN', 'Full system access', NOW(), 1),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567802', 'Admin', 'ADMIN', 'Administrative access', NOW(), 1),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567803', 'HRManager', 'HRMANAGER', 'HR management access', NOW(), 1),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567804', 'Manager', 'MANAGER', 'Team management access', NOW(), 1),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567805', 'Employee', 'EMPLOYEE', 'Standard employee access', NOW(), 1),
  ('a1b2c3d4-e5f6-7890-abcd-ef1234567806', 'ReadOnly', 'READONLY', 'Read-only access', NOW(), 1)
ON CONFLICT ("Id") DO NOTHING;

-- Admin User (password: Admin@123)
INSERT INTO "ApplicationUsers" ("Id", "UserName", "Email", "NormalizedUserName", "NormalizedEmail", "PasswordHash", "EmailConfirmed", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount", "FirstName", "LastName", "CreatedAt", "Status")
VALUES
  ('b1b2c3d4-e5f6-7890-abcd-ef1234567801', 'admin@hrms-pro.com', 'admin@hrms-pro.com', 'ADMIN@HRMS-PRO.COM', 'ADMIN@HRMS-PRO.COM', 'AQAAAAIAAYagAAAAEMz8x1G+g8Fz9k2L3m4N5p6Q7r8S9t0U1v2W3x4Y5z6A7b8C9d0E1f2G3h4I5==', true, false, false, false, 0, 'System', 'Admin', NOW(), 1)
ON CONFLICT ("Id") DO NOTHING;

-- Admin Role Assignment
INSERT INTO "UserRoles" ("UserId", "RoleId", "CreatedAt", "Status")
SELECT 'b1b2c3d4-e5f6-7890-abcd-ef1234567801', "Id", NOW(), 1
FROM "Roles" WHERE "Name" = 'SuperAdmin'
AND NOT EXISTS (SELECT 1 FROM "UserRoles" WHERE "UserId" = 'b1b2c3d4-e5f6-7890-abcd-ef1234567801');

-- Default Company
INSERT INTO "Companies" ("Id", "Name", "Code", "Industry", "Size", "Website", "CreatedAt", "Status")
VALUES
  ('c1b2c3d4-e5f6-7890-abcd-ef1234567801', 'HRMS Pro Demo Company', 'HRMS-DEMO', 'Technology', 'Medium', 'https://hrmspro.com', NOW(), 1)
ON CONFLICT ("Id") DO NOTHING;

-- Departments
INSERT INTO "Departments" ("Id", "Name", "Code", "CompanyId", "CreatedAt", "Status")
VALUES
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Human Resources', 'HR', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', NOW(), 1),
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567802', 'Engineering', 'ENG', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', NOW(), 1),
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567803', 'Marketing', 'MKT', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', NOW(), 1),
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567804', 'Finance', 'FIN', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', NOW(), 1),
  ('d1b2c3d4-e5f6-7890-abcd-ef1234567805', 'Operations', 'OPS', 'c1b2c3d4-e5f6-7890-abcd-ef1234567801', NOW(), 1)
ON CONFLICT ("Id") DO NOTHING;

-- Designations
INSERT INTO "Designations" ("Id", "Name", "Code", "Level", "CreatedAt", "Status")
VALUES
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567801', 'CEO', 'CEO', 'Executive', NOW(), 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567802', 'CTO', 'CTO', 'Executive', NOW(), 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567803', 'VP Engineering', 'VPE', 'VP', NOW(), 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567804', 'Senior Engineer', 'SR-ENG', 'Senior', NOW(), 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567805', 'Engineer', 'ENG', 'Mid', NOW(), 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567806', 'Junior Engineer', 'JR-ENG', 'Junior', NOW(), 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567807', 'HR Manager', 'HR-MGR', 'Manager', NOW(), 1),
  ('e1b2c3d4-e5f6-7890-abcd-ef1234567808', 'HR Coordinator', 'HR-CRD', 'Mid', NOW(), 1)
ON CONFLICT ("Id") DO NOTHING;

-- Grades
INSERT INTO "Grades" ("Id", "Name", "Code", "MinSalary", "MaxSalary", "CreatedAt", "Status")
VALUES
  ('f1b2c3d4-e5f6-7890-abcd-ef1234567801', 'Executive', 'G1', 150000, 300000, NOW(), 1),
  ('f1b2c3d4-e5f6-7890-abcd-ef1234567802', 'Senior', 'G2', 80000, 150000, NOW(), 1),
  ('f1b2c3d4-e5f6-7890-abcd-ef1234567803', 'Mid-Level', 'G3', 50000, 80000, NOW(), 1),
  ('f1b2c3d4-e5f6-7890-abcd-ef1234567804', 'Junior', 'G4', 30000, 50000, NOW(), 1)
ON CONFLICT ("Id") DO NOTHING;

-- Leave Types
INSERT INTO "LeaveTypes" ("Id", "Name", "Code", "DefaultDays", "IsCarryForward", "MaxCarryForwardDays", "IsPaid", "Description", "CreatedAt", "Status")
VALUES
  ('11b2c3d4-e5f6-7890-abcd-ef1234567801', 'Annual Leave', 'AL', 20, true, 5, true, 'Yearly vacation leave', NOW(), 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567802', 'Sick Leave', 'SL', 12, false, 0, true, 'Medical leave', NOW(), 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567803', 'Personal Leave', 'PL', 5, false, 0, true, 'Personal time off', NOW(), 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567804', 'Maternity Leave', 'ML', 90, false, 0, true, 'Maternity leave', NOW(), 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567805', 'Paternity Leave', 'PTL', 15, false, 0, true, 'Paternity leave', NOW(), 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567806', 'Bereavement Leave', 'BL', 5, false, 0, true, 'Bereavement leave', NOW(), 1),
  ('11b2c3d4-e5f6-7890-abcd-ef1234567807', 'Work From Home', 'WFH', 10, false, 0, true, 'Remote work days', NOW(), 1)
ON CONFLICT ("Id") DO NOTHING;

-- Attendance Statuses (if Attendance module uses a status table)
-- These are typically enums, but seeding policy defaults

-- Holidays
INSERT INTO "Holidays" ("Id", "Name", "Date", "Description", "IsRecurring", "CreatedAt", "Status")
VALUES
  ('21b2c3d4-e5f6-7890-abcd-ef1234567801', 'New Year', '2025-01-01', 'New Year Day', true, NOW(), 1),
  ('21b2c3d4-e5f6-7890-abcd-ef1234567802', 'Independence Day', '2025-07-04', 'Independence Day', true, NOW(), 1),
  ('21b2c3d4-e5f6-7890-abcd-ef1234567803', 'Labor Day', '2025-09-01', 'Labor Day', true, NOW(), 1),
  ('21b2c3d4-e5f6-7890-abcd-ef1234567804', 'Thanksgiving', '2025-11-27', 'Thanksgiving Day', true, NOW(), 1),
  ('21b2c3d4-e5f6-7890-abcd-ef1234567805', 'Christmas', '2025-12-25', 'Christmas Day', true, NOW(), 1)
ON CONFLICT ("Id") DO NOTHING;

-- Notification Templates
INSERT INTO "NotificationTemplates" ("Id", "Name", "Type", "Subject", "Body", "CreatedAt", "Status")
VALUES
  ('31b2c3d4-e5f6-7890-abcd-ef1234567801', 'Leave Request', 'Email', 'Leave Request Submitted', 'Your leave request has been submitted for approval.', NOW(), 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567802', 'Leave Approved', 'Email', 'Leave Request Approved', 'Your leave request has been approved.', NOW(), 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567803', 'Leave Rejected', 'Email', 'Leave Request Rejected', 'Your leave request has been rejected.', NOW(), 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567804', 'Welcome', 'Email', 'Welcome to HRMS Pro', 'Welcome to HRMS Pro! Your account has been created.', NOW(), 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567805', 'Password Reset', 'Email', 'Password Reset Request', 'Click the link to reset your password.', NOW(), 1),
  ('31b2c3d4-e5f6-7890-abcd-ef1234567806', 'Payroll Generated', 'Email', 'Payslip Available', 'Your payslip for this month is now available.', NOW(), 1)
ON CONFLICT ("Id") DO NOTHING;
